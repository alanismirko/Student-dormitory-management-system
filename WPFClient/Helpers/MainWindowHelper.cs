using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.view_model
{
    public class MainWindowHelper
    {
        private static readonly MainWindowHelper instance = new MainWindowHelper();
        public Action CloseAction { get; set; }
        
        private MainWindowHelper()
        {

        }
        public static MainWindowHelper Instance
        {
            get
            {
                return instance;
            }
        }
    }

}

