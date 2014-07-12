using ModQ.Models;

namespace ModQ.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ModQ.Models.QuizDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ModQ.Models.QuizDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.QuizModels.AddOrUpdate(i=>i.Question,
                new QuizModel
                {
                    Faculty = "Computing",
                    Module = "CG1103",
                    Question = "What is the function of 'virtual' keyword in objcect oriented programming languages like c++?",
                    FirstOption = "Provide Dynamic binding",
                    SecondOption = "Provide static binding",
                    ThirdOption = "Enables operator overloading",
                    FourthOption = "Enables operator overriding",
                    Answer = "Provide Dynamic binding"
                },
                new QuizModel
                {
                    Faculty = "Computing",
                    Module = "CS1231",
                    Question = "What is Herbrand's universe or universe of discourse or domain of discourse?",
                    FirstOption = "Collection of terms that contain only constants",
                    SecondOption = "Collection of terms that do not contain variables",
                    ThirdOption = "Collection of terms that contain only variables",
                    FourthOption = "Collection of terms that do not contain constants",
                    Answer = "Collection of terms that do not contain variables"
                },
                new QuizModel
                {
                    Faculty = "Science",
                    Module = "MA1505",
                    Question = "What is IVT (Intermediate Value Theorem)?",
                    FirstOption = "We can find a point c in [a,b] such that f(c) = y where function f(x) is continuous in a closed interval [a,b] and for any value y in range f(a) <= y <= f(b)",
                    SecondOption = "IVT cannot be applied to continuous funcitons",
                    ThirdOption = "It is not possible to show the polynomial has a real root between a and b",
                    FourthOption = "We can find a point c in [a,b] such that f(c) = y where function f(x) is continuous in a open interval (a,b) and for any value y in range f(a) < y < f(b)",
                    Answer = "We can find a point c in [a,b] such that f(c) = y where function f(x) is continuous in a closed interval [a,b] and for any value y in range f(a) <= y <= f(b)"
                },
                new QuizModel
                {
                    Faculty = "Science",
                    Module = "PC1432",
                    Question = "What happens to Electric flux when linear dimensions of the box containing charge q is doubled?",
                    FirstOption = "Flux becomes one fourth of original value",
                    SecondOption = "Flux remains same",
                    ThirdOption = "Flux becomes twice the original value",
                    FourthOption = "Flux ibecomes four times the original valueof the box",
                    Answer = "Flux remains same"
                },
                new QuizModel
                {
                    Faculty = "Science",
                    Module = "PC1432",
                    Question = "When excess charge is placed on a solid conductor and is at rest then?",
                    FirstOption = "excess charge resides on the interior",
                    SecondOption = "excess charge becomes zero",
                    ThirdOption = "excess charge resides on the surface",
                    FourthOption = "excess charge becomes infinite",
                    Answer = "excess charge resides on the surface"
                },
                new QuizModel
                {
                    Faculty = "Engineering",
                    Module = "CG1108",
                    Question = "What is Karnaugh Map (K-map)?",
                    FirstOption = "Graphical representation of truth table",
                    SecondOption = "each row in truth table corresponds to one cell i K-map",
                    ThirdOption = "adjacent (horizontal or vertical) cells have only one variable changing from 0 to 1 or vice versa",
                    FourthOption = "all of the above",
                    Answer = "all of the above"
                },
                new QuizModel
                {
                    Faculty = "Engineering",
                    Module = "CG1108",
                    Question = "When is principle of Superposition applicable to cirucits?",
                    FirstOption = "linear elements and non-linear controlled sources",
                    SecondOption = "linear elements and linear controlled sources",
                    ThirdOption = "non-linear elements and linear controlled sources",
                    FourthOption = "non-linear elements and non-linear controlled sources",
                    Answer = "linear elements and linear controlled sources"
                });
        }
    }
}
