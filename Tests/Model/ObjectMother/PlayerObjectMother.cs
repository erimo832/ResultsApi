using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Model.ObjectMother
{
    public class PlayerObjectMother
    {
        public static Player GetPlayer()
        {
            return new Player
            {
                FirstName = "John",
                LastName = "Smith",
                PDGANumber = null,
                Rounds = new List<Round>()
            };
        }
    }
}
