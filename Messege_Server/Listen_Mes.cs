using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messege_Server
{
    class Listen_Mes
    {
        private string Messege;
        private string Mes_Kod;

        Listen_Mes()
        {           
        }
        private void Start (string Mes)
        {
            this.Messege = Mes;
        }
        private string Messeg_text(string Mes)
        {
            string Text = "";
            this.Mes_Kod = Mes[0] + Mes[1] + "";
            for (int i =2; i < Mes.Length; i++)
            {
                Text += Mes[i];
            }

            return Text;
        }
        private void Rozpodil()
        {
            string Text_M = Messeg_text(this.Messege);
            if (this.Mes_Kod == "01")
            {

            }else if (this.Mes_Kod == "02")
            {
               
            }
            
            
        }

    }
}
