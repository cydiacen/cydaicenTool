using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cydiacenTool
{
    public partial class CreateFile : Form
    {
        public CreateFile()
        {
            InitializeComponent();
            
        }
        private string ModuleName => textBox1.Text;

        private string ServicesContent(string file = "")
        {
            return @"
""use strict"";
/**
 * author :CydiacenTool
 * time: " + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") + @"
 * description: 服务集合
*/

define([
    ""angular""
], function(angular) {
                return angular.module(""" + ModuleName + file + @"App.services"", [])
                    .service(""" + ModuleName + file + @"AppServices"", [
                        ""$rootScope"", ""$http"", ""accountNetService"",
                        function($rootScope, $http, accountNetService) {
                    var " + ModuleName + file + @"Services = { };
                    " + ModuleName + file + @"Services.services = function() {
                        var servicesConfig = { }
                        return servicesConfig;
                };
                return " + ModuleName + file + @"Services;
            }
        ]);
});
   
";
        }

        private string ControllerContent(string file = "")
        {
            return @"

""use strict"";
/**
 * author :CydiacenTool
 * time:" + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") + @"
 * description: 模块控制器
 */
define([""angular""], function(angular) {
                return angular.module(""" + ModuleName+file + @"App.controllers"", [])
                    .controller(""" + ModuleName + file + @"AppController"", [
                        ""$scope"", ""$rootScope"", ""$state"",
                        function($scope, $rootScope, $state) {
                    console.log(""" + ModuleName + file + @"App"");

                }
        ]);
});

            ";
        }

        private string GetFilePath()
        {
            var fileArr = textBox2.Text.Split('|');
            var returnStr = "";
            foreach (var file in fileArr)
            {
                returnStr += ("    \"modules/" + ModuleName + "-"+ file.ToLower()+ "-app/app\",\r\n");
            }
            return returnStr;
        }

        private string GetModuleDependency()
        {
            var fileArr = textBox2.Text.Split('|');
            var returnStr = "";
            foreach (var file in fileArr)
            {
                returnStr += (@"
                        """ + ModuleName + file+@"App"",");
            }
            return returnStr;
        }

        private string AppContent()
        {
            
            return @"
""use strict"";
/**
 * author :CydiacenTool
 * time:"+DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")+@"
 * description: 模块描述
 */


define([
    ""angular"",
    'uiRouter',
    ""modules/"+ ModuleName + @"-app/controller"",
    //挂载子模块
"+ GetFilePath()+
    @"""datePicker""
], function(angular) {

                return angular.module(""" + ModuleName + @"App"", [
                        ""ui.router"",
                        " + GetModuleDependency()+
                        @"
                ])
                    .config([
                        '$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
                $stateProvider
                    .state('" + ModuleName + @"', {
                        url: '/" + ModuleName + @"',
                        //abstract: true,
                        title: '模块',
                        templateUrl: 'modules/" + ModuleName + @"-app/" + ModuleName + @".html',
                        controller: '" + ModuleName + @"AppController'
                    });
                }
        ]);
});
            ";
        }

        private string SubAppContent(string file)
        {

            return @"
""use strict"";
/**
 * author :CydiacenTool
 * time:" + DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss") + @"
 * description: 子模块描述
 */


define([
    ""angular"",
    'uiRouter',
    ""modules/" + ModuleName+"-"+file.ToLower() + @"-app/controller"",
    ""datePicker""
], function(angular) {

                return angular.module(""" + ModuleName+file + @"App"", [
                        ""ui.router"",
                        """ + ModuleName+file + @"App.controllers"",
                ])
                    .config([
                        '$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
                $stateProvider
                    .state('" + ModuleName+"."+file.ToLower() + @"', {
                        url: '/" + file + @"',
                        //abstract: true,
                        title: '模块',
                        templateUrl: 'modules/" + ModuleName+"-"+file.ToLower() + @"-app/" + ModuleName+"."+file.ToLower() + @".html',
                        controller: '" + ModuleName+file + @"AppController'
                    });
                }
        ]);
});
            ";
        }

        private void CreateSubModule()
        {
            var fileArr = textBox2.Text.Split('|');
            foreach (var file in fileArr)
            {
                var appDir = @"D:\Tool\" + ModuleName + @"\" + ModuleName+"-"+ file.ToLower() + "-app";
                Directory.CreateDirectory(appDir);
                var nameArr = new [] { "\\app.js", "\\controller.js", "\\services.js" };
                foreach (var name in nameArr)
                {
                    var sw = new StreamWriter(appDir + name, false);
                    switch (name)
                    {
                        case "\\app.js":
                            sw.WriteLine(SubAppContent(file));
                            break;
                        case "\\controller.js":
                            sw.WriteLine(ControllerContent(file));
                            break;
                        case "\\services.js":
                            sw.WriteLine(ServicesContent(file));
                            break;
                    }
                    sw.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show(@"))请输入模块名称！");
            }
            string path = @"D:\Tool\" + textBox1.Text;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var appDir = path + @"\" + textBox1.Text + "-app";
                Directory.CreateDirectory(appDir);
                var nameArr = new [] { "\\app.js", "\\controller.js", "\\services.js" };
                foreach (var name in nameArr)
                {
                   var sw = new StreamWriter(appDir + name, false);
                    switch (name)
                    {
                        case "\\app.js":
                            sw.WriteLine(AppContent());
                            break;
                        case "\\controller.js":
                            sw.WriteLine(ControllerContent());
                            break;
                        case "\\services.js":
                            sw.WriteLine(ServicesContent());
                            break;
                    }
                    sw.Close();
                }
                CreateSubModule();
                MessageBox.Show(@"生成成功！请前往D盘Tool文件夹查看");
            }
            else
            {
                MessageBox.Show(@"该模块已在文件中存在！请删除后重试");
            }
        }
    }
}
