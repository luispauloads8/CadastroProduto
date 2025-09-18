using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Nucleo.MetodoExtensao
{
    public static class Extensao
    {

        private static readonly decimal _umCentimetroEmPoints = 28.3465M;
        private static readonly decimal _umCentimetroEmMilimetros = 10M;

        public static float CentimetrosToPoints(this decimal medidaEmCentimetros) => (float)(medidaEmCentimetros * _umCentimetroEmPoints);

        public static float MilimetrosToPoints(this decimal medidaEmMilimetros) => CentimetrosToPoints(medidaEmMilimetros / _umCentimetroEmMilimetros);

        public static float PointsToCentimetros(this decimal medidaEmPoints) => (float)(medidaEmPoints / _umCentimetroEmPoints);

        public static float PointsToMilimetros(this decimal medidaEmPoints) => (float)(((decimal)PointsToCentimetros(medidaEmPoints)) * _umCentimetroEmMilimetros);
    }
}
