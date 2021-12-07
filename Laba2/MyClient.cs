using Laba1;

namespace Laba2
{
    public class MyClient : HttpClientBase
    {
        public MyClient(string hostUrl) : base(hostUrl)
        {
        }

        public bool Ping()
        {
            return this.MakeGetRequest("ping").Contains("Ok");
        }

        public void Post(Input input)
        {
            this.MakePostRequest("postinputdata", input);
        }

        public Output Get()
        {
            return this.MakeGetRequest<Output>("getanswer");
        }

        public void Stop()
        {
            this.MakeGetRequest("stop");
        }
    }
}