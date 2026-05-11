using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassStore.Domain.Entities;

namespace ClassStore.WebUI.Models
{
    public class ClassesListViewModel
    {
        public IEnumerable<Class> Classes { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}