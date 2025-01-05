using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MachineLearningModel
{
    class Program
    {
        // Data structure for input
        public class PersonData
        {
            [LoadColumn(0)] public float Age { get; set; }
            [LoadColumn(1)] public float EducationYears { get; set; }
            [LoadColumn(2)] public float HoursWorked { get; set; }
            [LoadColumn(3)] public bool IncomeAbove50K { get; set; }
        }

        // Data structure for predictions
        public class IncomePrediction
        {
            [ColumnName("PredictedLabel")] public bool PredictedIncome { get; set; }
            public float Probability { get; set; }
        }

        static void Main(string[] args)
        {
            // Step 1: Create ML context
            MLContext mlContext = new MLContext();

            // Step 2: Load data
            string dataPath = "data.csv"; // Replace with your data path
            IDataView data = mlContext.Data.LoadFromTextFile<PersonData>(
                path: dataPath,
                hasHeader: true,
                separatorChar: ',');

            // Step 3: Split data into training and testing sets
            var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            var trainingData = trainTestSplit.TrainSet;
            var testingData = trainTestSplit.TestSet;

            // Step 4: Define the pipeline
            var pipeline = mlContext.Transforms.Concatenate("Features", new[] { "Age", "EducationYears", "HoursWorked" })
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "IncomeAbove50K", featureColumnName: "Features"));

            // Step 5: Train the model
            Console.WriteLine("Training the model...");
            var model = pipeline.Fit(trainingData);

            // Step 6: Evaluate the model
            Console.WriteLine("Evaluating the model...");
            var predictions = model.Transform(testingData);
            var metrics = mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "IncomeAbove50K");

            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:P2}");

            // Step 7: Use the model for predictions
            var samplePerson = new PersonData
            {
                Age = 35,
                EducationYears = 12,
                HoursWorked = 40
            };

            var predictionEngine = mlContext.Model.CreatePredictionEngine<PersonData, IncomePrediction>(model);
            var prediction = predictionEngine.Predict(samplePerson);

            Console.WriteLine($"Predicted Income: {(prediction.PredictedIncome ? ">50K" : "<=50K")}");
            Console.WriteLine($"Probability: {prediction.Probability:P2}");
        }
    }
}