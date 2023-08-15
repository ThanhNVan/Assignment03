using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.EntityProviders;

public class PagingModel
{
    #region [ Properties ]
    public int TotalItems { get; private set; }

    public int CurrentPage { get; private set; }

    public int PageSize { get; private set; }

    public int ToltalPages { get; private set; }

    public int StartPage { get; private set; }

    public int EndPage { get; private set; }
    #endregion

    #region [ CTor ]
    public PagingModel() {

    }

    public PagingModel(int totalItems, int currentPage, int pageSize) {
        int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
        int startPage = currentPage - 5;
        int endPage = currentPage + 4;

        if (startPage <= 0) {
            endPage = endPage - (startPage - 1);
            startPage = 1;
        }

        this.ToltalPages = totalPages;
        this.CurrentPage = currentPage;
        this.PageSize = pageSize;
        this.ToltalPages = totalPages;
        this.StartPage = startPage;
        this.EndPage = endPage;
    }
    #endregion
}
