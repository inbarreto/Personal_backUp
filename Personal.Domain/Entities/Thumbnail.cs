using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Domain.Entities
{
    public class Thumbnail
    {
        public string small { get; set; }
        public string large { get; set; }
        public string movil{ get; set; }
        public string hd{ get; set; }
        public string[] fanart { get; set; }
        public string crt { get; set; }
        

        public Thumbnail(bool variable)
        {
            small =@"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/medium.jpg";
            large = @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/large.jpg";
            movil = @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/mobile.jpg";
            hd = @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/medium.jpg";
            fanart = new string[] { @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/fanart_1.jpg", @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/tv.jpg", @"http://streamer.qubit.tv/contents/b858cfe5-daa0-47d2-88b0-8e496b3150eb_BlackDog/thumbnails/tv.jpg" };
            crt = string.Empty;

        }

        public Thumbnail()
        {
            small = string.Empty;
            large = string.Empty;
            movil = string.Empty;
            hd = string.Empty;
            fanart = new string[2];
            crt = string.Empty;

        }


    }
}

