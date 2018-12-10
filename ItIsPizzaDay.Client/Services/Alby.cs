namespace ItIsPizzaDay.Client.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abstract;

    class Alby : IAlby
    {
        static readonly IEnumerable<string> albyThoughts = new List<string>
        {
            // Cosing
            "Cosa?",
            "Non ho capito",
            "Che cosa vuol dire?",

            // Programming
            "Chi vuole vedere del bel codice?",
            "quanti magnum vuoi per farlo?",
            "roald mi fixi un bug?",
            "basta che usi un albipattern",
            "hai mai sentito parlare della michael jackson solution?",
            "mi fixi un bug sull\"app delle pizze ? ",
            "cosa devo fare ? il computer non va",
            "blocco la schedina",
            "facciamo un identity server ? ",

            // Tennis
            "ti sfido a tennis",

            // Bisiacchery
            "non sono bisiacco",

            // Misc
            "Cioè non è possibile",
            "ti do un magnum",
            "sai che toglieranno la macchinetta del caffè?",
            "ma si dice porcina? dai come cazzo si dice",
            "Scommettiamo?? quanto scommettiamo?",

            // Alby
            "Alberto 1 Roald 0",
            "Giorgio is a tennis looser",
            "Come si chiama questa mano? TrueTennis!",
            "Come si chiama l\"altra mano? TrueProgramming"
        };

        readonly Random random = new Random();

        static bool WorthMatching(string s1, string s2)
            => s1 != null && s2 != null && s1.Length > 3 && s2.Length > 3;

        double WeakSentenceMatch(string s1, string s2)
        {
            if (!WorthMatching(s1, s2))
            {
                return 0;
            }

            var comparisons = 
                from w1 in s1.Split(' ') 
                from w2 in s2.Split(' ') 
                select WeakWordsCompare(w1, w2);
            
            return comparisons.Sum();
        }

        static bool AreContained(string w1, string w2)
            => w1.Contains(w2) || w2.Contains(w1);

        static int WeakWordsCompare(string w1, string w2)
        {
            if (!WorthMatching(w1, w2))
            {
                return 0;
            }

            var affinity = 0;
            for (var i1 = 3; i1 < w1.Length; i1++)
            {
                for (var i2 = 3; i2 < w2.Length; i2++)
                {
                    affinity += AreContained(w1.Substring(startIndex: 0, i1), w2.Substring(startIndex: 0, i2)) ? 1 : 0;
                }
            }

            return affinity;
        }

        AlbyReply ExploreThoughts(string text)
        {
            var bestMatch = AlbyReply.Default;

            foreach (var thought in albyThoughts)
            {
                var tempAffinity = WeakSentenceMatch(text, thought) * GetRandomArbitrary(min: 1, max: 3);
                if (tempAffinity > bestMatch.Affinity)
                {
                    bestMatch = new AlbyReply(thought, tempAffinity);
                }
            }

            return bestMatch;
        }

        bool CosaProbability()
            => GetRandomArbitrary(min: 1, max: 3) > 2.5;

        double GetRandomArbitrary(double min, double max)
            => random.NextDouble() * (max - min) + min;

        public string Reply(string text)
            => (CosaProbability() ? AlbyReply.Default : ExploreThoughts(text)).Text;

        class AlbyReply
        {
            public AlbyReply(string text, double affinity)
            {
                Text = text;
                Affinity = affinity;
            }

            public string Text { get; }
            public double Affinity { get; }

            public static AlbyReply Default { get; } = new AlbyReply("Cosa?", affinity: 0);
        }
    }
}