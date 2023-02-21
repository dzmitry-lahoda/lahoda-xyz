﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StaThreadSyncronizer
{
   internal class StaThread
   {
      private Thread mStaThread;
      private IQueueReader<SendOrPostCallbackItem> mQueueConsumer;
      private int mThreadID;
      private ManualResetEvent mStopEvent = new ManualResetEvent(false);


      internal StaThread(IQueueReader<SendOrPostCallbackItem> reader)
      {
         mQueueConsumer = reader;
         mStaThread = new Thread(Run);
         mStaThread.Name = "STA Worker Thread";
         mStaThread.SetApartmentState(ApartmentState.STA);
      }

      internal int ManagedThreadId
      {
         get
         {
            return mThreadID;
         }
      }
      

      internal void Start()
      {
         mStaThread.Start();
      }


      internal void Join()
      {
         mStaThread.Join();
      }

      private void Run()
      {
         mThreadID = Thread.CurrentThread.ManagedThreadId;
         while (true)
         {
            bool stop = mStopEvent.WaitOne(0);
            if (stop)
            {
               break;
            }

           SendOrPostCallbackItem workItem = mQueueConsumer.Dequeue();
           if (workItem != null)
               workItem.Execute();
         }
      }

      internal void Stop()
      {
         mStopEvent.Set();
         mQueueConsumer.ReleaseReader();
         mStaThread.Join();
         mQueueConsumer.Dispose();         
      }
   }
}
