using System;
using System.Threading.Tasks; 
using System.Drawing;

namespace Exemplo.Terminal.ParalelismoForeach
{
    class Program
    {
        static void Main(string[] args)
        {
            // Caso necessario modifique este caminho.
            String[] files = System.IO.Directory.GetFiles(@"C:\Users\Public\Pictures", "*.jpg");
            String newDir = @"C:\Users\Public\Pictures\Modificado";

            Console.WriteLine(new String('*', 100));
            Console.WriteLine("Neste exemplo vamos utilizar o Parallel.ForEach para girar as imagens dentro de uma pasta");
            Console.WriteLine(new String('*', 100));

            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();

            stop.Start();

            System.IO.Directory.CreateDirectory(newDir);

            // Assinatura metodo: Parallel.ForEach(IEnumerable<TSource> source, Action<TSource> body)
            // OBSERVACAO: Certifique-se de adicionar uma referência ao System.Drawing.dll
            Parallel.ForEach(files, (currentFile) =>
            {
                // Quanto mais trabalho computacional você fizer aqui, maior                                     
                // o aumento de velocidade comparado a um loop foreach sequencial.
                String fileName = System.IO.Path.GetFileName(currentFile);
                var bitmap = new Bitmap(currentFile);

                // faz rotação das imagens em 180 graus
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                bitmap.Save(System.IO.Path.Combine(newDir, fileName));

                // 

                Console.WriteLine("Processando {0} na Thread {1}", fileName, System.Threading.Thread.CurrentThread.ManagedThreadId);

                // final da expressão lambda
            });

            stop.Stop();

            // obtem o valor Timespan para elapsed
            TimeSpan ts = stop.Elapsed;

            // formata e apresenta o valor do TimeSpan
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Tempo de Execução Geral.: " + elapsedTime);


            // pula a console do windows
            Console.WriteLine("Processamento completado com sucesso. Pressione [ENTER] para Sair.");
            Console.ReadKey(); 
        }
    }
}
