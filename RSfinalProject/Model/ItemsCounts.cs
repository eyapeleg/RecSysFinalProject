﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSfinalProject
{
    public class ItemsCounts
    {
        Dictionary<string, int> counts;

        public ItemsCounts()
        {
            this.counts = new Dictionary<string, int>();
        }

        public void addItem(string item)
        {
            if (!counts.ContainsKey(item))
            {
                counts.Add(item, 0);
            }
            counts[item]++;
        }

        public int getItemCount(string item)
        {
            if (counts.ContainsKey(item))
                return counts[item];
            else
                return 0;
        }

        public List<string> getItemsList()
        {
            return this.counts.Keys.ToList();
        }
    }
}
