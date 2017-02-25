using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework
{
  public  class AccountGridResponseModel<T> where T:class
    {
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }

    }


    public class PagedResults<T>
    {

        
        /// <summary>
        /// The page number this page represents.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The size of this page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalNumberOfPages { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int TotalNumberOfRecords { get; set; }

        /// <summary>
        /// The URL to the next page - if null, there are no more pages.
        /// </summary>
        public string NextPageUrl { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        public IEnumerable<T> Results { get; set; }
    }


    public class GridRequestModel
    {

        public GridRequestModel()
        {
            Filters = new SortedList<string, string>();
        }

        public Nullable<Int32> Page { get; set; }

        public Nullable<Int32> PageSize { get; set; }

        public string SortName { get; set; }
        public bool SortDirection { get; set; }

        public SortedList<string, string> Filters { get; set; }

    }
}
