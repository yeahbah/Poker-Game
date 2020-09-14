using System;
using System.Runtime.Serialization;

namespace PokerGames.VideoPoker
{
    [Serializable]
    internal class VideoPokerException : Exception
    {
        public VideoPokerException()
        {
        }

        public VideoPokerException(string message) : base(message)
        {
        }

        public VideoPokerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VideoPokerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}