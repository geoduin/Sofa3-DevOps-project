﻿using Sofa3Devops.SprintStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Domain
{
    public class Developer : Member
    {
        public bool Seniority { get; set; } = false;


        public Developer(string name, string emailAddress, string slackUserName) : base(name, emailAddress,
            slackUserName)
        {
        }


        public void SetLeadDeveloper()
        {
            Seniority = true;
        }

        public override void ApproveAndFinishItem(BacklogItem item)
        {
            if(Seniority)
            {
                item.SetItemToFinished();
            }
            else
            {
                base.ApproveAndFinishItem (item);
            }
            
        }

        public override void DisapproveTestedItem(BacklogItem item)
        {
            
            if (Seniority)
            {
                item.SetItemReadyForTesting();
            }
            else
            {
                base.DisapproveTestedItem(item);
            }
            
        }
    }
}
