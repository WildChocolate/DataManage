using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class RoleMenu
    {
        public int Key;
        public string Menu;
        public string ParentMenu;
        public bool CanReaded;
        public bool CanUpdated;
        public bool CanCreated;
        public bool CanDeleted;
    }
    public class RoleMenuGrid
    {
        public RoleMenuGrid() 
        {
//            title = new List<th>();
//            title.Add(new th { field="Key", title="Key"});
//            title.Add(new th { field = "Menu", title = "菜单" });
//            title.Add(new th { field = "ParentMenu", title = "父级"});
//            title.Add(new th
//            {
//                field = "CanReaded",
//                title = "浏览",
//                formatter = @"
//                        function (value, rowData, rowIndex) {
//	  							if(!value){
//	  								return '<input type='checkbox'  id='a_'+rowIndex+'' />';
//	  							}
//                                else(value)
//	  								return '<input type='checkbox'   id='a_'+rowIndex+'' checked/>';
//                        "
//            });
//            title.Add(new th
//            {
//                field = "CanUpdated",
//                title = "编辑",
//                formatter = @"
//                        function (value, rowData, rowIndex) {
//	  							if(!value){
//	  								return '<input type='checkbox'  id='b_'+rowIndex+'' />';
//	  							}
//                                else(value)
//	  								return '<input type='checkbox'   id='b_'+rowIndex+'' checked/>';
//                        "
//            });
//            title.Add(new th
//            {
//                field = "CanCreated",
//                title = "添加",
//                formatter = @"
//                        function (value, rowData, rowIndex) {
//	  							if(!value){
//	  								return '<input type='checkbox'  id='c_'+rowIndex+'' />';
//	  							}
//                                else(value)
//	  								return '<input type='checkbox'   id='c_'+rowIndex+'' checked/>';
//                        "
//            });
//            title.Add(new th
//            {
//                field = "CanDeleted",
//                title = "删除",
//                formatter = @"
//                        function (value, rowData, rowIndex) {
//	  							if(!value){
//	  								return '<input type='checkbox'  id='d_'+rowIndex+'' />';
//	  							}
//                                else(value)
//	  								return '<input type='checkbox'   id='d_'+rowIndex+'' checked/>';
//                        "
//            });    
        }
        //public List<th> title;
        public int total;
        public List<RoleMenu> rows;
    }
}
