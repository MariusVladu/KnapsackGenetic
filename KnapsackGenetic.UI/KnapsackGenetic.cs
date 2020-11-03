using KnapsackGenetic.Algorithm;
using KnapsackGenetic.Algorithm.Contracts;
using KnapsackGenetic.Domain;
using KnapsackGenetic.Greedy;
using KnapsackGenetic.Providers;
using KnapsackGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KnapsackGenetic.UI
{
    public partial class KnapsackGenetic : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        private IFitnessFunction fitnessFunction;
        private ISelectionOperator selectionOperator;
        private IElitistSelection elitistSelection;
        private ICrossoverOperator crossoverOperator;
        private IMutationOperator mutationOperator;
        private IInitialPopulationProvider initialPopulationProvider;
        private Settings settings;

        private DataTable itemsTable;

        private List<double> generationsPlotData;
        private List<double> averageScorePlotData;
        private List<double> bestScorePlotData;

        public KnapsackGenetic()
        {
            InitializeComponent();

            itemsTable = new DataTable();
            itemsTable.Columns.Add("Weight");
            itemsTable.Columns.Add("Value");
            DisplayItems();

            fitnessFunction = new FitnessFunction();
            selectionOperator = new TournamentSelection(Convert.ToInt32(inputTournamentSize.Value));
            elitistSelection = new ElitistSelection();
            crossoverOperator = new CrossoverOperator();
            mutationOperator = new MutationOperator();
            initialPopulationProvider = new InitialPopulationProvider();

            InitializeGeneticAlgorithm();

            chartAverageScore.plt.XLabel("Generation #");
            chartAverageScore.plt.YLabel("Average Fitness Score");
            chartBestScore.plt.XLabel("Generation #");
            chartBestScore.plt.YLabel("Best Fitness Score");
            Plot();
        }

        private void DisplayItems()
        {
            var items = GetInitialItems();

            foreach (var item in items)
            {
                itemsTable.Rows.Add(item.Weight, item.Value);
            }

            inputItems.DataSource = itemsTable;
        }

        private List<Item> GetItemsList()
        {
            var items = new List<Item>();

            foreach (DataRow row in itemsTable.Rows)
            {
                items.Add(new Item
                {
                    Weight = Convert.ToInt32(row["Weight"]),
                    Value = Convert.ToInt32(row["Value"])
                });
            }

            return items;
        }

        private void InitializeGeneticAlgorithm()
        {
            var items = GetItemsList();

            settings = new Settings
            {
                Items = items,
                NumberOfGenes = items.Count,
                WeightLimit = Convert.ToInt32(inputWeightLimit.Value),
                NumberOfElites = Convert.ToInt32(inputElites.Value),
                InitialPopulationSize = Convert.ToInt32(inputMaxPopulation.Value),
                MaxPopulationSize = Convert.ToInt32(inputMaxPopulation.Value),
                CrossoverRate = Convert.ToDouble(inputCrossoverRate.Value),
                MutationRate = Convert.ToDouble(inputMutationRate.Value)
            };

            selectionOperator = new TournamentSelection(Convert.ToInt32(inputTournamentSize.Value));

            geneticAlgorithm = new GeneticAlgorithm(settings, initialPopulationProvider, fitnessFunction, selectionOperator, elitistSelection, crossoverOperator, mutationOperator);

            generationsPlotData = new List<double>();
            averageScorePlotData = new List<double>();
            bestScorePlotData = new List<double>();
            UpdatePlotData();
        }

        private void buttonNextGeneration_Click(object sender, EventArgs e)
        {
            geneticAlgorithm.ComputeNextGeneration();

            UpdatePlotData();
            Plot();
        }

        private void UpdatePlotData()
        {
            generationsPlotData.Add(geneticAlgorithm.CurrentGenerationNumber);
            averageScorePlotData.Add(geneticAlgorithm.AverageScore);
            bestScorePlotData.Add(geneticAlgorithm.CurrentBestSolution.FitnessScore);
        }

        private void Plot()
        {
            var generationsPlotArray = generationsPlotData.ToArray();

            chartAverageScore.plt.Clear();
            chartAverageScore.plt.PlotScatter(generationsPlotArray, averageScorePlotData.ToArray(), Color.Blue);
            chartAverageScore.plt.AxisAuto();
            chartAverageScore.Render();

            chartBestScore.plt.Clear();
            chartBestScore.plt.PlotScatter(generationsPlotArray, bestScorePlotData.ToArray(), Color.Green);
            chartBestScore.plt.AxisAuto();
            chartBestScore.Render();

            ShowBestSolution();
        }

        private void ShowBestSolution()
        {
            var generationNumberString = geneticAlgorithm.CurrentGenerationNumber.ToString().PadLeft(4, '0'); ;
            var averageScore2DecimalPlaces = string.Format("{0:00.00}", geneticAlgorithm.AverageScore);
            labelGenerationInfo.Text = $"Generation #{generationNumberString}\nAverage: {averageScore2DecimalPlaces}\nBest Solution: {geneticAlgorithm.CurrentBestSolution}\nBest Score: {geneticAlgorithm.CurrentBestSolution.FitnessScore}";
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();

            var stopwatch = new Stopwatch();

            for (int i = 0; i < inputGenerationsNumber.Value; i++)
            {
                stopwatch.Start();
                geneticAlgorithm.ComputeNextGeneration();
                stopwatch.Stop();

                UpdatePlotData();

                if (i % 50 == 0) Plot();
            }

            Plot();
            DisplayEllapsedTime(stopwatch.Elapsed);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();
            Plot();
        }

        private List<Item> GetInitialItems()
        {
            return new List<Item>
            {
                new Item{ Weight = 7, Value = 5},
                new Item{ Weight = 2, Value = 4},
                new Item{ Weight = 1, Value = 7},
                new Item{ Weight = 9, Value = 2},
                new Item{ Weight = 20, Value = 5},
                new Item{ Weight = 11, Value = 6},
                new Item{ Weight = 2, Value = 6},
                new Item{ Weight = 15, Value = 10},
                new Item{ Weight = 3, Value = 1},
                new Item{ Weight = 4, Value = 2},
                new Item{ Weight = 8, Value = 5},
            };
        }

        private void buttonGreedySolution_Click(object sender, EventArgs e)
        {
            var knapsackGreedy = new KnapsackGreedy();
            var items = GetItemsList();
            var weightLimit = Convert.ToInt32(inputWeightLimit.Value);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var solution = knapsackGreedy.Solve(items, weightLimit);

            stopwatch.Stop();
            
            var totalValue = 0;
            for (int i = 0; i < solution.Length; i++)
                if(solution[i] == true)
                    totalValue += items[i].Value;
            labelGenerationInfo.Text = $"Greedy solution: {string.Join(", ", solution.Select(x => x ? 1 : 0))}\nTotal Value: {totalValue}";
            DisplayEllapsedTime(stopwatch.Elapsed);
        }

        private void DisplayEllapsedTime(TimeSpan elapsedTime)
        {
            labelGenerationInfo.Text += $"\nElapsed Time: {elapsedTime.TotalMilliseconds} ms";
        }
    }
}
