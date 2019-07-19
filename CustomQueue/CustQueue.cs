// ******************************************************************************************************************
//  Custom Queue that persists data via a database.
//  Copyright(C) 2019  James LoForti
//  Contact Info: jamesloforti@gmail.com
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//
using System;
using System.Collections.Generic;

namespace CustomQueue
{
    public class CustQueue // no IQueue<T> interface in core, only framework
    {
        private Queue<Node> _queue;

        public CustQueue()
        {
            _queue = new Queue<Node>();
        } // end ctor

        public Node Serve()
        {
            //Try to get the node at the front of the line
            if (_queue.TryDequeue(out Node node))
            {
                //Remove node from database if dequeue attempt was successful
            }

            //If time to live is up
            if (node.Ttl > DateTime.Now)
            {
                return node;
            }
            else // ttl is NOT up
            {
                Append(node);
            }

            return null;
        } // end method Serve

        public void Append(Node node)
        {
            //Write to database
            
            //Write to local queue
            _queue.Enqueue(node);

            //Process changes and attempt to re-process existing nodes
            //Cycle();
        } // end method Append

        public void Cycle()
        {
            //While the next node's ttl is NOT up and it hasn't been re-processed yet
            while (_queue.Peek().Ttl < DateTime.Now && _queue.Peek().Visited == false)
            {
                //Mark node as visited
                _queue.Peek().Visited = true;
                //Attempt to process the next node
                Serve();
            }
        } // end method Cycle

        public void ResetVisitors()
        {
            foreach (var n in _queue)
            {
                n.Visited = false;
            }
        } // end method ResetVisitors

        public void Delay()
        {
            //if size of queue changed (use delegate)
            //serve front
            //check ttl
            //if ttl not expired, mark node as visited
            //append node to back of queue
            //iterate over queue and serve nodes (while marking as visited) until node with visited==true is found
            //then set all nodes' visited flags to false and wait for the next queue size change callback
        }
    }
}
