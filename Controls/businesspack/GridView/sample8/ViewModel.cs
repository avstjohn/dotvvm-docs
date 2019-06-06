using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.BusinessPack.Controls;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotvvmWeb.Views.Docs.Controls.businesspack.GridView.sample8
{
    public class ViewModel : DotvvmViewModelBase
    {
        public BusinessPackDataSet<Order> Orders { get; set; }
        public List<string> DeliveryTypes { get; set; } = new List<string> { "Post office", "Home" };

        public override Task Init()
        {
            Orders = new BusinessPackDataSet<Order> {
                PagingOptions = {
                    PageSize = 10
                },
                RowEditOptions = new RowEditOptions {
                    PrimaryKeyPropertyName = nameof(Order.Id),
                    EditRowId = -1
                }
            };

            return base.Init();
        }

        public override Task Load()
        {
            if (Orders.IsRefreshRequired)
            {
                Orders.LoadFromQueryable(GetQueryable(15));
            }

            return base.Load();
        }

        private IQueryable<Order> GetQueryable(int size)
        {
            var numbers = new List<Order>();
            for (var i = 0; i < size; i++)
            {
                numbers.Add(new Order { Id = i + 1, DeliveryType = DeliveryTypes[(i + 1) % 2], IsPaid = (i + 1) % 2 == 0, CreatedDate = DateTime.Now.AddDays(-i) });
            }
            return numbers.AsQueryable();
        }

    }
}
