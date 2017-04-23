using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameInfo
{
    public static class DummyMessages
    {

        static List<string> messages = new List<string>() {
            "Curabitur semper lectus et erat cursus vehicula.",
            "Sed sed mi ac neque ultrices efficitur eu sagittis est.",
            "Nulla venenatis turpis sed consequat dignissim.",
            "Phasellus iaculis urna eget nisl porta vestibulum volutpat accumsan ipsum.",
            "Proin ullamcorper ex vel commodo tincidunt.",
            "Nulla ac nulla euismod massa pellentesque porttitor vel et justo.",
            "Maecenas semper turpis quis nunc scelerisque, ut mattis tellus venenatis.",
            "Morbi nec massa sed orci euismod tempor.",
            "Etiam id purus sed ligula tincidunt aliquam.",
            "Sed a ligula condimentum, ullamcorper justo ac, efficitur velit.",
            "Praesent elementum dolor eget libero vulputate pulvinar.",
            "Aenean quis sapien vestibulum, imperdiet mauris a, posuere leo.",
            "Morbi eget arcu vel felis maximus bibendum.",
            "Cras dignissim sapien luctus venenatis ornare.",
            "Maecenas sodales neque in est viverra, a euismod tortor condimentum.",
            "Nulla nec nisi mollis, maximus dolor sit amet, rutrum elit.",
            "Suspendisse ornare augue ac turpis dignissim cursus.",
            "Cras dapibus sapien at ante iaculis, sit amet laoreet quam vehicula.",
            "Proin vitae velit non libero condimentum tristique.",
            "Proin aliquet dui non eros dignissim ullamcorper.",
            "Ut sit amet dui in nisl maximus finibus a nec tortor.",
            "Morbi varius mi sagittis ante elementum, vitae dignissim nisi volutpat.",
            "Vestibulum condimentum nisi fermentum risus varius, ac tempor justo volutpat.",
            "Donec ultrices quam ac elementum vulputate.",
            "Pellentesque quis massa at justo dapibus consectetur id in quam."
        };

        public static string GetRandomString()
        {
            return messages[Random.Range(0, Constants.maxMessages)];

        }


    }
}