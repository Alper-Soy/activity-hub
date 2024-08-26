using System.Text.Json;

namespace API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int currentPage,
        int itemsPerPage, int totalItems, int totalPages)
    {
        var paginationHeader = new
        {
            currentPage,
            itemsPerPage,
            totalItems,
            totalPages
        };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
