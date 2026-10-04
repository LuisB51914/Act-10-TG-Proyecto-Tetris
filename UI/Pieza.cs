using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using Tetris2D.UI;       // <-- Esto soluciona el error (le dice dónde buscar TemaArcade)
using Tetris2D.Graficos; // <-- Recomendado por si tu tema usa elementos gráficos

namespace Tetris2D.UI
{
    public enum TipoPieza
    {
        I, O, T, S, Z, J, L
    }

    public class Pieza
    {
        // Corregido: 'Y' en mayúscula para mantener consistencia
        public IReadOnlyList<(int X, int Y)> Bloques { get; private set; }
        public TipoPieza Tipo { get; }
        public Vector4 Color { get; }

        // Corregido: La sintaxis del diccionario y el arreglo de tuplas
        private static readonly IReadOnlyDictionary<TipoPieza, (int X, int Y)[]> FormasBase =
            new Dictionary<TipoPieza, (int X, int Y)[]>
            {
            { TipoPieza.I, new (int X, int Y)[] { (0, 1), (1, 1), (2, 1), (3, 1) } },
            { TipoPieza.O, new (int X, int Y)[] { (1, 1), (2, 1), (1, 2), (2, 2) } },
            { TipoPieza.T, new (int X, int Y)[] { (1, 0), (0, 1), (1, 1), (2, 1) } },
            { TipoPieza.S, new (int X, int Y)[] { (1, 0), (2, 0), (0, 1), (1, 1) } },
            { TipoPieza.Z, new (int X, int Y)[] { (0, 0), (1, 0), (1, 1), (2, 1) } },
            { TipoPieza.J, new (int X, int Y)[] { (0, 0), (0, 1), (1, 1), (2, 1) } },
            { TipoPieza.L, new (int X, int Y)[] { (2, 0), (0, 1), (1, 1), (2, 1) } }
            };

        private static readonly IReadOnlyDictionary<TipoPieza, Vector4> Colores =
            new Dictionary<TipoPieza, Vector4>
            {
                [TipoPieza.I] = TemaArcade.Cian,
                [TipoPieza.O] = TemaArcade.Amarillo,
                [TipoPieza.T] = TemaArcade.Magenta,
                [TipoPieza.S] = TemaArcade.Verde,
                [TipoPieza.Z] = TemaArcade.Rojo,
                [TipoPieza.J] = TemaArcade.Azul,
                [TipoPieza.L] = TemaArcade.Naranja
            };

        public Pieza(TipoPieza tipo)
        {
            Tipo = tipo;
            Color = Colores[tipo];
            Bloques = FormasBase[tipo];
        }

        public List<(int X, int Y)> ObtenerRotacion(bool horario)
        {
            var resultado = new List<(int X, int Y)>(Bloques.Count);
            foreach ((int x, int y) in Bloques)
            {
                resultado.Add(horario ? (3 - y, x) : (y, 3 - x));
            }
            return resultado;
        }

        public void AplicarRotacion(bool horario)
        {
            Bloques = ObtenerRotacion(horario);
        }

        public static Pieza Aleatoria(Random rnd)
        {
            TipoPieza tipo = (TipoPieza)rnd.Next(7);
            return new Pieza(tipo);
        }
    }
}


