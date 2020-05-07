using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Cupcake.Core;
using Cupcake.Core.Plugins;
using Cupcake.Plugin.TaskModule.Services;
using Cupcake.Plugin.TaskModule.Data;
using Cupcake.Services;

namespace Cupcake.Plugin.TaskModule
{
    public class TemplateService : BasePlugin
    {
        #region Fields

        private readonly ITaskIssuedService _googleService;
        private readonly ITaskDisposalService _googleService2;
        private readonly PluginContext _objectContext;

        #endregion

        #region Ctor
        public TemplateService(ITaskIssuedService googleService, ITaskDisposalService googleService2,
            PluginContext objectContext)
        {
            this._googleService = googleService;
            this._googleService2 = googleService2;
            this._objectContext = objectContext;
        }

        #endregion



        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            this.AddMenu("任务下发", "/TaskIssued");
            this.AddMenu("任务处置", "/TaskDisposal");
            //data
            _objectContext.Install();            

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //this.RemoveMenuAtRoot("任务下发", "/TaskIssued");
            //this.RemoveMenuAtRoot("任务处置", "/TaskDisposal");
            //data
            _objectContext.Uninstall();

            base.Uninstall();
        }
    }
}
