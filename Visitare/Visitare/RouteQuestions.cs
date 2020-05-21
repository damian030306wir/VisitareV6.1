using System;
using System.Collections.Generic;
using System.Text;

namespace Visitare
{
     public class RouteQuestions
    {
        public RouteQuestions(List<Question> list)
        {
            routeQuestions = list;
        }
        public List<Question> routeQuestions { get; set; }



    }
}
