using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Core.Domain.Specification;

public class AndSpecification<T> : BaseSpecification<T> where T : IEntity
{

    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;

        ApplyCriteria();
        
        Includes = [.. left.Includes ?? [], .. right.Includes ?? []];
            
        ThenIncludes = [.. left.ThenIncludes ?? [], .. right.ThenIncludes ?? []];

        var orderBy = _left.OrderBy ?? _right.OrderBy;

        if( orderBy != null )
        {
            ApplyOrderBy(orderBy );
        }

        var orderByDescending = _left.OrderByDescending ?? _right.OrderByDescending;

        if(orderByDescending  != null)
        {
            ApplyOrderByDescending(orderByDescending);
        }

        if (_left.IsDistinct && _right.IsDistinct)
        {
            Distincted();
        }

        if (_left.IsSpliting || _right.IsSpliting)
            ActiveSpliting();
           
    }

    private void ApplyCriteria()
    {
        var leftExpression = _left.Criteria;
        var rightExpression = _right.Criteria;

        if (leftExpression == null && rightExpression != null)
        {
            SetCriteria(rightExpression);
            return;
        }
        
        if(leftExpression != null && rightExpression == null)
        {
            SetCriteria(leftExpression);
            return;
        }
        
        if(leftExpression == null && rightExpression == null)
            return;
        
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpression!, parameter),
            Expression.Invoke(rightExpression!, parameter)
        );

        var expression = Expression.Lambda<Func<T, bool>>(body, parameter);
        SetCriteria(expression);
    }


       
}