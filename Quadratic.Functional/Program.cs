using LanguageExt;
using static LanguageExt.Prelude;

var coefficients = (a: 1.0, b: -3.0, c: 2.0); // Пример: уравнение x^2 - 3x + 2 = 0
var results = SolveQuadraticEquation(coefficients);

results.Match(
    Some: roots => Console.WriteLine($"Roots: {string.Join(", ", roots)}"),
    None: () => Console.WriteLine("Error: No real roots exist or invalid equation.")
);

static Option<double[]> SolveQuadraticEquation((double a, double b, double c) coefficients)
{
    return ValidateCoefficients(coefficients)
        .Bind(CalculateDiscriminantOrLinear)
        .Bind(CalculateRoots);
}

static Option<(double a, double b, double c)> ValidateCoefficients((double a, double b, double c) coefficients)
{
    var (a, b, c) = coefficients;
    return a == 0 && b == 0 ? Option<(double a, double b, double c)>.None : Some((a, b, c));
}

static Option<(double a, double b, double c, Option<double> discriminant)> CalculateDiscriminantOrLinear((double a, double b, double c) coefficients)
{
    var (a, b, c) = coefficients;

    if (a == 0)
    {
        // Handle linear equation bx + c = 0
        return b != 0 ? Some((a, b, c, Option<double>.None)) : Option<(double a, double b, double c, Option<double> discriminant)>.None;
    }

    // Handle quadratic equation
    var discriminant = b * b - 4 * a * c;
    return Some((a, b, c, Some(discriminant)));
}

static Option<double[]> CalculateRoots((double a, double b, double c, Option<double> discriminant) data)
{
    var (a, b, _, optDiscriminant) = data;

    if (a == 0)
    {
        // Solve linear equation bx + c = 0
        var root = -data.c / b;
        return Some(new[] { root });
    }

    // Solve quadratic equation
    return optDiscriminant.Match(
        Some: discriminant =>
        {
            if (discriminant < 0)
            {
                return Option<double[]>.None;
            }

            var sqrtDiscriminant = Math.Sqrt(discriminant);
            var root1 = (-b + sqrtDiscriminant) / (2 * a);
            var root2 = (-b - sqrtDiscriminant) / (2 * a);
            return Some(new[] { root1, root2 });
        },
        None: () => Option<double[]>.None
    );
}