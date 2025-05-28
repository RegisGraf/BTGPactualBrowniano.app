using BTGPactualBrowniano.app.Models.Base;

namespace BTGPactualBrowniano.app.Models
{
    public class Cores : ModelBase
    {
        private Color cor = Colors.Blue;
        public Color Cor
        {
            get { return cor; }
            set
            {
                cor = value;
                OnPropertyChanged("Cor");
            }
        }

        private string corHexa = "#3f48cc";
        public string CorHexa
        {
            get { return corHexa; }
            set
            {
                corHexa = value;
                OnPropertyChanged("CorHexa");
            }
        }
    }
}
