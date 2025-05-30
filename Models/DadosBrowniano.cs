using BTGPactualBrowniano.app.Models.Base;
using System.Collections.ObjectModel;

namespace BTGPactualBrowniano.app.Models
{
    public class DadosBrowniano : ModelBase
    {
        private double precoInicial = 100D;
        public double PrecoInicial
        {
            get { return precoInicial; }
            set
            {
                precoInicial = value;
                OnPropertyChanged("PrecoInicial");
            }
        }

        private string strPrecoInicial = "R$ 100,00";
        public string StrPrecoInicial
        {
            get { return strPrecoInicial; }
            set
            {
                strPrecoInicial = value;
                OnPropertyChanged("StrPrecoInicial");

                if (!string.IsNullOrEmpty(value))
                    PrecoInicial = Utils.Converters.ConvetStringToDouble(value, 2);
            }
        }

        private double volatilidade = 20;
        public double Volatilidade
        {
            get { return volatilidade; }
            set
            {
                volatilidade = value;
                OnPropertyChanged("Volatilidade");
            }
        }

        private double retornoMedio = 10;
        public double RetornoMedio
        {
            get { return retornoMedio; }
            set
            {
                retornoMedio = value;
                OnPropertyChanged("RetornoMedio");
            }
        }

        private int numeroDias = 50;
        public int NumeroDias
        {
            get { return numeroDias; }
            set
            {
                numeroDias = value;
                OnPropertyChanged("NumeroDias");
            }
        }

        private ObservableCollection<PointF> points = new ObservableCollection<PointF>();
        public ObservableCollection<PointF> Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged("Points");
            }
        }

        private Color corDaLinha = Colors.Blue;
        public Color CorDaLinha
        {
            get { return corDaLinha; }
            set
            {
                corDaLinha = value;
                OnPropertyChanged("CorDaLinha");
            }
        }

        private string corDaLinhaHexa = Colors.Blue.ToHex();
        public string CorDaLinhaHexa
        {
            get { return corDaLinhaHexa; }
            set
            {
                corDaLinhaHexa = value;
                OnPropertyChanged("CorDaLinhaHexa");
            }
        }

        private int serie = 1;
        public int Serie
        {
            get { return serie; }
            set
            {
                serie = value;
                OnPropertyChanged("Serie");
            }
        }
    }
}
