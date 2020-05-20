using System;
using System.Collections.Generic;
using System.Text;

namespace Visitare
{
     public class RouteQuestions
    {
        public RouteQuestions(List<Question> list)
        {
            routeQuestion = list;
        }
        public List<Question> routeQuestion { get; set; }



    }
}
