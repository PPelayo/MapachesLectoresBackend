﻿using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBooksUseCase(
    IRepository<Book> bookRepository     
)
{
    public async Task<PaginationResult<Book>> InvokeAsync(IPagintaion pagintaion)
    {
        var spec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories());
        
        var books = await bookRepository.GetAsync(pagintaion.ToQueryPagination(), spec);

        return books.ToPaginationResult(pagintaion.ToQueryPagination());
    }
}