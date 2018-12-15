﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumoPhoton
{
    public class Team
    {
        public List<ClientPeer> clientPeers = new List<ClientPeer>();
        public int masterRoleId = 0;
        

        public Team()
        {
            
        }

        public Team(ClientPeer peer1, ClientPeer peer2, ClientPeer peer3)
        {
            clientPeers.Add(peer1);
            clientPeers.Add(peer2);
            clientPeers.Add(peer3);
            peer1.Team = this;
            peer2.Team = this;
            peer3.Team = this;
            masterRoleId = peer3.LoginRole.Id; //把客户端主机的角色的Id储存起来
        }

        public void Dismiss()
        {
            masterRoleId = 0;
            foreach (ClientPeer peer in clientPeers)
            {
                peer.Team = null;
            }
            clientPeers = null;

        }
    }
}
