using BTGPactualBrowniano.app.Models;
using System.Collections.ObjectModel;

namespace BTGPactualBrowniano.app.Renderers
{
    public class BrownianDrawable : IDrawable
    {
        private ObservableCollection<DadosBrowniano> _series = new ();
        private float _scaleX, _scaleY;
        private float _maxPrice = float.MinValue;
        private float _minPrice = float.MaxValue;
        private int _numDias;
        private float _margin = 50;
        
        //Calcula os pontos de variação de preço
        public void CalcularBrownianoFinanceiro(int numeroDias, double precoInicial, double volatilidade, double retornoMedio, string corDaLinhaHexa, int serie, bool atualizaAposExclusao = false)
        {
            DadosBrowniano novaSerie = new DadosBrowniano()
            {
                Points = new ObservableCollection<PointF>(),
                CorDaLinhaHexa = corDaLinhaHexa,
                NumeroDias = numeroDias,
                PrecoInicial = precoInicial,
                RetornoMedio = retornoMedio,
                Volatilidade = volatilidade,
                Serie = serie
            };

            _maxPrice = float.MinValue;
            _minPrice = float.MaxValue;
            
            double[] prices = new double[novaSerie.NumeroDias];
            prices[0] = novaSerie.PrecoInicial;

            // Converte % para decimal
            novaSerie.Volatilidade /= 100;
            novaSerie.RetornoMedio /= 100;

            double preco = novaSerie.PrecoInicial;
            novaSerie.Points.Add(new PointF(0, (float)preco));

            for (int i = 1; i < novaSerie.NumeroDias; i++)
            {
                double z = RandomExtensions.NextGaussian(new());
                double retornoDiario = novaSerie.RetornoMedio + novaSerie.Volatilidade * z;

                prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
                novaSerie.Points.Add(new PointF(i, (float)prices[i]));
            }

            if (_series == null)
                _series = new ObservableCollection<DadosBrowniano>() { novaSerie };
            else
                _series.Add(novaSerie);

            _numDias = novaSerie.NumeroDias;
            var max = _series.SelectMany(serie => serie.Points).Max(p => p.Y);
            var min = _series.SelectMany(serie => serie.Points).Min(p => p.Y);
            _maxPrice = max;
            _minPrice = min;
        }

        //remove a serie selecionada e recalcula os maximos e minimos de preços e maximo de dias
        public void RemoverSerie(DadosBrowniano serie)
        {
            if (serie is not null && _series.Any())
            {
                var res = _series.Single(s => s.Serie == serie.Serie);
                _series.Remove(res);

                var max = _series.SelectMany(serie => serie.Points).Max(p => p.Y);
                var min = _series.SelectMany(serie => serie.Points).Min(p => p.Y);
                _maxPrice = max;
                _minPrice = min;

                _numDias = _series.Max(s => s.NumeroDias);
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {

            if (_series.Count == 0) return;

            CalculateScales(dirtyRect);

            canvas.FillColor = Color.FromArgb("#F8F9FA");
            canvas.FillRectangle(dirtyRect);

            DrawGrid(canvas, dirtyRect);
            DrawAxisLabels(canvas, dirtyRect);
            DrawPricePath(canvas);
        }

        //Calcula escalas
        private void CalculateScales(RectF dirtyRect)
        {
            float drawWidth = dirtyRect.Width - 2 * _margin;
            float drawHeight = dirtyRect.Height - 2 * _margin;

            _scaleX = drawWidth / _numDias;
            _scaleY = drawHeight / (_maxPrice - _minPrice);
        }

        //monta linhas de faixa de preço
        private void DrawGrid(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.LightGray;
            canvas.StrokeSize = 0.5f;

            // Linhas verticais (dias)
            for (int dia = 0; dia <= _numDias; dia += _numDias / 10)
            {
                float x = _margin + dia * _scaleX;
                canvas.DrawLine(x, _margin, x, dirtyRect.Height - _margin);
            }

            // Linhas horizontais (preços)
            float priceStep = (_maxPrice - _minPrice) / 10;
            for (float price = _minPrice; price <= _maxPrice; price += priceStep)
            {
                float y = dirtyRect.Height - _margin - (price - _minPrice) * _scaleY;
                canvas.DrawLine(_margin, y, dirtyRect.Width - _margin, y);
            }
        }

        //monta os valores dos eixos Y(preços) e X(dias)
        private void DrawAxisLabels(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 10;

            // Rótulos do eixo X (dias)
            for (int dia = 0; dia <= _numDias; dia += _numDias / 10)
            {
                float x = _margin + dia * _scaleX;
                canvas.DrawString(dia.ToString(), x, dirtyRect.Height - _margin + 20, HorizontalAlignment.Center);
            }

            // Rótulos do eixo Y (preços)
            float priceStep = (_maxPrice - _minPrice) / 10;
            for (float price = _minPrice; price <= _maxPrice; price += priceStep)
            {
                float y = dirtyRect.Height - _margin - (price - _minPrice) * _scaleY;
                canvas.DrawString(price.ToString("N2"), _margin - 50, y, HorizontalAlignment.Left);
            }

            // Títulos dos eixos
            canvas.FontSize = 12;
            canvas.DrawString("Dias", dirtyRect.Width / 2, dirtyRect.Height - 10, HorizontalAlignment.Center);
            canvas.DrawString($"Preço (R$)", 1, _margin / 2, HorizontalAlignment.Left);
        }

        //monta as linhas das series
        private void DrawPricePath(ICanvas canvas)
        {
            foreach (var serie in _series)
            {
                canvas.StrokeColor = Color.FromArgb(serie.CorDaLinhaHexa);
                canvas.StrokeSize = 2;

                var path = new PathF();
                path.MoveTo(_margin, ScaleY(serie.Points[0].Y));

                for (int i = 1; i < serie.Points.Count; i++)
                {
                    float x = _margin + serie.Points[i].X * _scaleX;
                    float y = ScaleY(serie.Points[i].Y);
                    path.LineTo(x, y);
                }

                canvas.DrawPath(path);
            }
        }

        private float ScaleY(float price)
        {
            return _margin + (_maxPrice - price) * _scaleY;
        }
    }

    // Extensão para gerar números normais randomicos para usar na simulação de calculo de variação de preço
    public static class RandomExtensions
    {
        public static double NextGaussian(this Random rand)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        }
    }
}
