﻿namespace BTGPactualBrowniano.app.Utils
{
    public enum TiposEntry
    {
        Default,
        Texto,      // Padrão
        Numerico,   // Apenas números
        Moeda,      // Formatação R$ 9.999,99
        Inteiro     // Apenas numeros
    }

    public enum TiposLinhas
    {
        Continua,
        Tracejada,
        TracejadaLonga,
        tracejadaCurta,
        Mista,
        Pontilhada
    }
}
