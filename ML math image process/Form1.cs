using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp; 
using OpenCvSharp.Extensions; 
using Microsoft.ML; 
using Microsoft.ML.Data;

namespace ML_math_image_process
{
    public partial class Form1 : Form
    {

        Bitmap drawingBitmap;
        Graphics graphics;
        bool isDrawing = false;
        System.Drawing.Point lastPoint = System.Drawing.Point.Empty;

        public Form1()
        {
            InitializeComponent();

            drawingBitmap = new Bitmap(PictureImage.Width, PictureImage.Height);
            graphics = Graphics.FromImage(drawingBitmap);
            graphics.Clear(Color.White); 
            PictureImage.Image = drawingBitmap;
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
               
                Pen pen = new Pen(Color.Black, 15);
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                graphics.DrawLine(pen, lastPoint, e.Location);
                PictureImage.Refresh(); 
                lastPoint = e.Location;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }


        private void Clean_button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            PictureImage.Refresh();
            //Clean_button1.Text = "Yeni çizim:";
        }

        private float[] ProcessImageForML(Bitmap originalImage)
        {
            using (Mat src = originalImage.ToMat())
            {

                using (Mat gray = new Mat())
                using (Mat resized = new Mat())
                {
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                    Cv2.BitwiseNot(gray, gray);

                    
                    Cv2.Resize(gray, resized, new OpenCvSharp.Size(28, 28));


                    float[] pixelData = new float[28 * 28];
                    var indexer = resized.GetGenericIndexer<byte>();

                    int i = 0;
                    for (int y = 0; y < 28; y++)
                    {
                        for (int x = 0; x < 28; x++)
                        {
                            pixelData[i] = indexer[y, x] / 255.0f;
                            i++;
                        }
                    }
                    return pixelData;
                }
            }
        }

        private float[] ApplySoftmax(float[] scores)
        {
            float[] probabilities = new float[scores.Length];
            double sum = 0;

            for (int i = 0; i < scores.Length; i++)
            {
                probabilities[i] = (float)Math.Exp(scores[i]);
                sum += probabilities[i];
            }

            for (int i = 0; i < scores.Length; i++)
            {
                probabilities[i] = probabilities[i] / (float)sum;
            }

            return probabilities;
        }


        public class InputData
        {
            
            [ColumnName("Input3")]
            [VectorType(784)]
            public float[] PixelValues { get; set; }
        }

        public class OutputData
        {
            
            [ColumnName("Plus214_Output_0")]
            public float[] Score { get; set; }
        }

        private void Tahmin_button1_Click(object sender, EventArgs e)
        {

            try
            {
                var result = PredictMultipleDigits(drawingBitmap);

                string text = result.Item1;
                float confidence = result.Item2;

                if (string.IsNullOrEmpty(text))
                {
                    Sonuc_label1.Text = "Sonuç: (Algılanamadı)";
                }
                else
                {
                    Sonuc_label1.Text = $"Tahmin: {text} (Güven: %{confidence * 100:0.00})";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            
        }


        private Mat PreprocessSingleDigit(Mat src)
        {
            int maxDim = 20;
            double scale = (double)maxDim / Math.Max(src.Width, src.Height);
            int newWidth = (int)(src.Width * scale);
            int newHeight = (int)(src.Height * scale);

            using (Mat resized = new Mat())
            {
                Cv2.Resize(src, resized, new OpenCvSharp.Size(newWidth, newHeight));

                Mat finalImage = new Mat(28, 28, MatType.CV_8UC1, new Scalar(0));

                int xOffset = (28 - newWidth) / 2;
                int yOffset = (28 - newHeight) / 2;

                var roi = new Rect(xOffset, yOffset, newWidth, newHeight);
                resized.CopyTo(finalImage[roi]);

                return finalImage; 
            }
        }

        private (string, float) PredictMultipleDigits(Bitmap fullImage)
        {
            MLContext mlContext = new MLContext();
            var pipeline = mlContext.Transforms.ApplyOnnxModel(
                    outputColumnName: "Plus214_Output_0",
                    inputColumnName: "Input3",
                    modelFile: "mnist.onnx");

            var emptyData = mlContext.Data.LoadFromEnumerable(new List<InputData>());
            var model = pipeline.Fit(emptyData);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputData>(model);

            string finalResult = "";
            float totalScore = 0; 
            int digitCount = 0;   

            using (Mat src = fullImage.ToMat())
            using (Mat gray = new Mat())
            using (Mat binary = new Mat())
            {
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.BitwiseNot(gray, binary);
                Cv2.Threshold(binary, binary, 50, 255, ThresholdTypes.Binary);

                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;

                Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                var sortedContours = contours
                    .Select(c => new { Contour = c, Rect = Cv2.BoundingRect(c) })
                    .OrderBy(x => x.Rect.X)
                    .ToList();

                foreach (var item in sortedContours)
                {
                    Rect rect = item.Rect;
                    if (rect.Width < 5 || rect.Height < 5) continue;

                    using (Mat cropped = new Mat(binary, rect))
                    using (Mat processed = PreprocessSingleDigit(cropped))
                    {
                        float[] pixelData = new float[28 * 28];
                        var indexer = processed.GetGenericIndexer<byte>();
                        for (int y = 0; y < 28; y++)
                        {
                            for (int x = 0; x < 28; x++)
                            {
                                pixelData[(y * 28) + x] = indexer[y, x] / 255.0f;
                            }
                        }

                        var prediction = predictionEngine.Predict(new InputData { PixelValues = pixelData });

                        float[] probs = ApplySoftmax(prediction.Score);
                        float maxConf = probs.Max(); 
                        int predictedNum = Array.IndexOf(probs, maxConf);

                        finalResult += predictedNum.ToString();
                        totalScore += maxConf; 
                        digitCount++;          
                    }
                }
            }

            if (digitCount == 0) return ("", 0);
            return (finalResult, totalScore / digitCount);
        }

    }
}
