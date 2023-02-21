using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MultiplayPingSample.Server.Utilities
{
    public class AsyncResult<T>
    {
        Func<AsyncOperation, T> m_AsyncCompletionHandler;
        Action<T> m_HandlerCompleted;
        Action<Exception> m_ExceptionHandler;
        UnityWebRequestAsyncOperation webRequestAsyncOperation;

        public AsyncResult(Func<AsyncOperation, T> asyncCompletionHandler)
        {
            m_AsyncCompletionHandler = asyncCompletionHandler;
        }

        public void Completed(AsyncOperation asyncOp)
        {
            try
            {
                Result = m_AsyncCompletionHandler(asyncOp);
                IsCompleted = true;
                m_HandlerCompleted?.Invoke(Result);

            }
            catch (Exception ex)
            {
                Error = ex;
                m_ExceptionHandler?.Invoke(ex);
            }
            finally
            {
                IsCompleted = true;
            }
        }

        public event Action<T> completed
        {
            add
            {
                if (IsCompleted)
                {
                    // If caller is registering for completed after result is available, call them back immediately
                    value.Invoke(Result);
                }
                else
                {
                    m_HandlerCompleted += value;
                }
            }
            remove { m_HandlerCompleted -= value; }
        }

        public event Action<Exception> exception
        {
            add
            {
                if (Error != null)
                {
                    // If caller is registering for exception after exception has been caught call them back immediately
                    value.Invoke(Error);
                }
                else
                {
                    m_ExceptionHandler += value;
                }
            }
            remove { m_ExceptionHandler -= value; }
        }

        public T Result { get; set; }
        public Exception Error { get; private set; }
        public bool IsCompleted { get; private set; }
    }
}
