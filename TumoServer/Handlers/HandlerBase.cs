﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using Photon.SocketServer;
using TumoCommon;

namespace TumoPhoton.Handlers
{
     public abstract class HandlerBase
     {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public HandlerBase()
         {
             TumoApplication.Instance.handlers.Add((byte)OpCode, this);
             log.Debug("Hanlder:" + this.GetType().Name + "  is register.");
         }

        public abstract void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer,SendParameters sendParameters);

        public abstract OperationCode OpCode { get; }

     }
}
