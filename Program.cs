using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace AutoCyberChatBot_Part2 //ST10278605 //Siphesihle Mavuso
{
    internal class Program
    {
        // Memory for user preferences
        static string userName = "";
        static string userInterest = "";

        // Random generator
        static Random random = new Random();

        // Keyword dictionary with sample responses
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string>
                {
                    "Use long, unique passwords with a mix of letters, numbers, and symbols.",
                    "Avoid using names, birthdays, or common words in your passwords.",
                    "Consider using a password manager to generate and store strong passwords."
                }
            },
            { "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal info — they could be phishing.",
                    "Don't click suspicious links or attachments in unsolicited messages.",
                    "Check the sender's email carefully; phishing emails often mimic real ones."
                }
            },
            { "privacy", new List<string>
                {
                    "Review app permissions and limit data sharing.",
                    "Use secure browsers and VPNs to enhance your privacy online.",
                    "Always log out from public or shared devices."
                }
            },
            { "scam", new List<string>
                {
                    "Ignore messages claiming you've won something out of the blue.",
                    "Scammers often create a sense of urgency — stay calm and verify.",
                    "Use two-factor authentication to protect your accounts from scams."
                }
            }
        };

        // Sentiment keywords and responses
        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            { "worried", "It's okay to feel worried — cybersecurity threats can be scary, but knowledge is power." },
            { "curious", "Curiosity is a great start! Ask me anything you'd like to learn about staying safe online." },
            { "frustrated", "I understand it's frustrating — cybersecurity can be complex, but I'm here to help step-by-step."}
        };

        static void Main(string[] args)
        {
            VoiceGreeting();
            LogoDisplay();

            Console.Write("Enter your name: ");
            userName = Console.ReadLine();
            Console.WriteLine($"\nHello {userName}, welcome to the Cybersecurity Awareness Chatbot!");

            while (true)
            {
                Console.WriteLine("\nAsk me about a Cybersecurity topic, or type 'exit' to quit:");
                Console.Write("You: ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "exit")
                {
                    Console.WriteLine("\nStay safe online! Goodbye!");
                    break;
                }

                ProcessUserInput(userInput);
            }
        }

        //VoiceGreeting 
        public static void VoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("greeting.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Error playing sound: " + ex.Message + "]");
            }
        }

        //LogoDisplay
        public static void LogoDisplay()
        {
            Console.WriteLine(@"
                                       .----. 
                                      ( o  o ) 
                                      |  --  |
                                      |______|
                                    /|        |\
                                   / |        | \
                                  *  *        *  *

                               CYBERSECURITY CHATBOT 
     ""Ensuring your safety by protecting our conversations one message at a Time""");
        }

        static void ProcessUserInput(string input)
        {
            // Sentiment detection
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    Console.WriteLine($"\n[Sentiment]: {sentimentResponses[sentiment]}");
                    return;
                }
            }

            // Memory storage
            if (input.Contains("interested in"))
            {
                int startIndex = input.IndexOf("interested in") + "interested in".Length;
                userInterest = input.Substring(startIndex).Trim();
                Console.WriteLine($"\nGreat! I'll remember that you're interested in {userInterest}.");
                return;
            }

            // Memory Recall 
            if (input.Contains("what am i interested in"))
            {
                if (!string.IsNullOrEmpty(userInterest))
                    Console.WriteLine($"\nYou told me earlier you're interested in {userInterest}.");
                else
                    Console.WriteLine("\nYou haven't told me your cybersecurity interests yet!");
                return;
            }

            // Keyword recognition and random responses
            bool matched = false;
            foreach (var keyword in keywordResponses.Keys)
            {
                if (input.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    var response = responses[random.Next(responses.Count)];
                    Console.WriteLine($"\n[{keyword.ToUpper()} Tip]: {response}");
                    matched = true;
                    break;
                }
            }

            // Memory aware personalization
            if (!matched && !string.IsNullOrEmpty(userInterest) && input.Contains("tip"))
            {
                if (keywordResponses.ContainsKey(userInterest))
                {
                    var responses = keywordResponses[userInterest];
                    var response = responses[random.Next(responses.Count)];
                    Console.WriteLine($"\nSince you're interested in {userInterest}, here's a tip: {response}");
                    matched = true;
                }
            }

            if (!matched)
            {
                Console.WriteLine("\nI'm not sure I understand. Can you try rephrasing?");
            }
        }
    }
}
