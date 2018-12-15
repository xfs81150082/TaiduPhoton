using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using System.IO;
using TumoPhoton.Handlers;
using System.Reflection;

namespace TumoPhoton
{   
    //继承自ApplicationBase的类，是server的入口程序，也就是启动程序
    class TumoApplication : ApplicationBase
    {
        private static TumoApplication _instance;
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();
        
        public static TumoApplication Instance
        {
            get { return _instance; }
        }

        public List<ClientPeer> clientPeerListFotTeam = new List<ClientPeer>();

        public TumoApplication()
        {
            _instance = this;
            RegisteHandlers();           
        }

        //客户端联接这个server服务器端时调用，(//网上资料：建立连线并回传给Photon Server)
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ClientPeer(initRequest);  //网上资料：建立连线并回传给PhotonServer
        }

        //当服务器端server自起初始化时调用
        protected override void Setup()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "Tm" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
            log.Debug("服务器启动成功...");
        }

        void RegisteHandlers()
        {
            //handlers.Add((byte) OperationCode.Login, new LoginHandler());       //把LoginHandler交给TumoApplication 来管理
            //handlers.Add((byte) OperationCode.GetServer,new ServerHandler());   //把GetServer交给TumoApplication 来管理
            //handlers.Add((byte) OperationCode.Register,new RegisterHandler());  //把。。。注册上
            //handlers.Add((byte) OperationCode.Role,new RoleHandler());          //把。。。注册上

            Type[] types = Assembly.GetAssembly(typeof(HandlerBase)).GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.EndsWith("Handler"))
                {
                    Activator.CreateInstance(type);
                }
            }
        } 

        //当这个服务器被停止的时候调用
        protected override void TearDown()
        {
            log.Debug("服务器停止运行...");
        }


    }
}
