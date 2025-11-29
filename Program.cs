namespace TheSeer
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Random randomCard = new Random();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Type($"\n\n\t\tGreetings, {Adjective()} One.");
            Thread.Sleep(1000);
            Type("\n\t\tOur name? A thing lost to time many eons ago - and not relevant for this time being.");
            Thread.Sleep(1000);
            Type("\n\n\t\tMore relevant, is what you would want us to call *you*.");
            Thread.Sleep(1000);
            Type("\n\t\tTell us now, child; What is your name?\n\n");

            string userName = Console.ReadLine()!; // added ! after tips online
            Console.Clear();
            NameGreeting(userName);
            Thread.Sleep(3000);

            bool appIsRunning = true;
            bool firstMeeting = true;

            while (appIsRunning)
            {
                Console.Clear();
                if (firstMeeting)
                {
                    Type($"\n\n\t\tSit down {userName}, and pick your path.");
                    Thread.Sleep(1000);
                    firstMeeting = false;
                }
                else
                    Type($"\n\n\t\tWell then, " + Adjective() + $" {userName}. What shall you do now?");
                Thread.Sleep(1000);

                Type($"\n\n\t\tThe [W]anderer");
                Type($"\n\t\tTwo [N]uns");
                Type($"\n\t\tThree [S]pies");
                Type($"\n\t\tFour [L]ords");
                Type($"\n\t\t[Q]uit\n\n");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                        SpreadSelector("Wanderer", userName, randomCard);
                        break;

                    case ConsoleKey.N:
                        SpreadSelector("Nuns", userName, randomCard);
                        break;

                    case ConsoleKey.S:
                        SpreadSelector("Spies", userName, randomCard);
                        break;

                    case ConsoleKey.L:
                        SpreadSelector("Lords", userName, randomCard);
                        break;

                    case ConsoleKey.Q:
                        Console.Clear();
                        Type("\n\n\t\tAs it was foretold. Fate has now been revealed. What remains is for you to walk its path.");
                        Type("\n\n\t\tI shall remain here if you wish to return.");
                        Thread.Sleep(5000);
                        appIsRunning = false;
                        break;

                    default:
                        Console.Clear();
                        Type("\n\n\t\tStop wasting my time, child, or I'll have the writers of fate smudge your yet drying ink.");
                        Thread.Sleep(3000);
                        break;
                }
            }
        }
        static string Adjective()
        {
            Random random = new Random();
            int adjectiveNum = random.Next(1, 16);

            switch (adjectiveNum)
            {
                case 1:
                    return "adorned";
                case 2:
                    return "feared";
                case 3:
                    return "embellished";
                case 4:
                    return "hunted";
                case 5:
                    return "loved";
                case 6:
                    return "awaited";
                case 7:
                    return "tired";
                case 8:
                    return "noble";
                case 9:
                    return "respected";
                case 10:
                    return "forgotten";
                case 11:
                    return "despised";
                case 12:
                    return "hunted";
                case 13:
                    return "exiled";
                case 14:
                    return "tarnished";
                case 15:
                    return "strong";
            }
            return "uhh...";
        }

        static void NameGreeting(string name)
        {
            Random random = new Random();
            int greetingNum = random.Next(1, 6);



            switch (greetingNum)
            {
                case 1:
                    Type($"\n\n\t\tAh... yes, of course, {name}, they told us you were coming.");
                    break;
                case 2:
                    Type($"\n\n\t\tHmpf, all right, if you do not want to give us your real name, {name} shall do.");
                    break;
                case 3:
                    Type($"\n\n\t\tA royal name on such a common face... a peasant looking {name}, how queer.");
                    break;
                case 4:
                    Type($"\n\n\t\tDark must the days be, when {name} seeks us out for knowledge and answers. Well then.");
                    break;
                case 5:
                    Type($"\n\n\t\tThe pleasure is wholly mine, {name}");
                    break;
            }
        }

        static void Type(string sentence)
        {
            foreach (var letter in sentence)
            {
                Console.Write(letter);
                Thread.Sleep(5);
            }
        }

        static void SpreadSelector(string spreadName, string userName, Random randomCard)
        {
            bool innerLoop = true;
            char yesOrNo;
            Console.Clear();

            if (spreadName == "Wanderer")
            {
                Type("\n\n\t\tThe Wanderer is a lonely spread. A single card, to answer a single, yet perhaps not simple question.");
                Type("\n\t\tShe may help you with one of two conundrums; a specific, yet not closed question... \n\t\tor if you\'d rather, she might help you gain some wisdom of the current day.");
                Type($"\n\t\tWould you like to walk with the Wanderer, {userName}?\n\t\t[Y/N]");



                while (innerLoop)
                {
                    yesOrNo = Console.ReadKey().KeyChar;

                    if (yesOrNo == 'y' || yesOrNo == 'Y')
                    {
                        Console.Clear();
                        Type("\n\n\t\tYou have chosen to walk alongside the Wanderer for a while. Hopefully a wise choice.");
                        Type("\n\t\tWithin your mind, or heart if you feel inclined, either ask the question you would like a general guidance for, or simply think:\n\n\t\t\"Wanderer, you who have walked both in past and future trails, show me the path of my day.\"");
                        Type($"\n\n\t\tWith your question or plea in your mind, press any button to let her show you the way, {userName}. . .");
                        Console.ReadKey();
                        TarotRead(1, "Wanderer", randomCard);
                        innerLoop = false;


                    }
                    else if (yesOrNo == 'n' || yesOrNo == 'N')
                    {
                        Console.Clear();
                        Type($"\n\n\t\tAhh, very well {userName}, the Wanderer shall keep wandering on her own.");
                        Thread.Sleep(3000);
                        innerLoop = false;
                    }
                    else
                    {
                        Console.Clear();
                        Type("\n\n\t\t...Yes or No, traveler.\n\t\t[Y/N]");
                    }
                }
            }
            else if (spreadName == "Nuns")
            {
                Type("\n\n\t\tThe sisterly nuns may show you duality. A chance to differ what one ought to do, and\n\t\twhat one ought to avoid. Their holy guidance will stop you from devilish actions, whilst leading you to more pious lands.");
                Type($"\n\t\tDo you desire holy guidance from the two nuns, {userName}?\n\t\t[Y/N]");

                innerLoop = true;

                while (innerLoop)
                {
                    yesOrNo = Console.ReadKey().KeyChar;

                    if (yesOrNo == 'y' || yesOrNo == 'Y')
                    {
                        Console.Clear();
                        Type("\n\n\t\tYou have accepted to take the two nuns by their hands. a holy choice, indeed.");
                        Type("\n\t\tWithin your mind, or heart if you feel inclined, hold the dilemma you wish to know what to do with, and also what to avoid doing, to reach your desired result.");
                        Type($"\n\n\t\tWith your dilemma held within you, press any button to let them pray for you, {userName}. . .");
                        Console.ReadKey();
                        TarotRead(2, "Nuns", randomCard);
                        innerLoop = false;


                    }
                    else if (yesOrNo == 'n' || yesOrNo == 'N')
                    {
                        Console.Clear();
                        Type($"\n\n\t\tAhh, very well {userName}, the two nuns will return to their other duties, then.");
                        Thread.Sleep(3000);
                        innerLoop = false;
                    }
                    else
                    {
                        Console.Clear();
                        Type("\n\n\t\t...Yes or No, traveler.\n\t\t[Y/N]");
                    }
                }

            }
            else if (spreadName == "Spies")
            {
                Type("\n\n\t\tThree shadowy spies I have at my disposal. These men do not spy in courts, but in fate, and destiny.");
                Type("\n\n\t\tOne gathers intel from your past, the other of your present, the third of your future.");
                Type($"\n\t\tThese three spies are for hire. What do you say, {userName}?\n\t\t[Y/N]");

                innerLoop = true;

                while (innerLoop)
                {
                    yesOrNo = Console.ReadKey().KeyChar;

                    if (yesOrNo == 'y' || yesOrNo == 'Y')
                    {
                        Console.Clear();
                        Type("\n\n\t\tThe missions have been accepted, the spies are ready to move.");
                        Type("\n\t\tWithin your mind, or heart if you feel inclined, hold the information you wish to have broadened, both within the past, present and future");
                        Type($"\n\n\t\tWith this held within you, press any button to let the spies report to you, {userName}. . .");
                        Console.ReadKey();
                        TarotRead(3, "Spies", randomCard);
                        innerLoop = false;


                    }
                    else if (yesOrNo == 'n' || yesOrNo == 'N')
                    {
                        Console.Clear();
                        Type($"\n\n\t\tI understand, {userName}. I shall send the spies to work for other clients.");
                        Thread.Sleep(3000);
                        innerLoop = false;
                    }
                    else
                    {
                        Console.Clear();
                        Type("\n\n\t\t...Yes or No, traveler.\n\t\t[Y/N]");
                    }
                }
            }
            else if (spreadName == "Lords")
            {
                Type("\n\n\t\tThe four lords rule over fiefs, but not fiefs of land... they oversee the four corners of our being: mind, body, spirit and subconsciousness.");
                Type("\n\t\tIf you desire, the lords might come to sit with us, to tell you what is going on in these four areas of yourself.");
                Type($"\n\t\tWill you meet with the four noble lords of your life, {userName}?\n\t\t[Y/N]");

                innerLoop = true;

                while (innerLoop)
                {
                    yesOrNo = Console.ReadKey().KeyChar;

                    if (yesOrNo == 'y' || yesOrNo == 'Y')
                    {
                        Console.Clear();
                        Type("\n\n\t\tThe lords have been called upon, and with them their knowledge, good or bad.");
                        Type("\n\t\tFind your cosmic middle. For the four lords to accurately serve you, you must serve yourself.");
                        Type($"\n\n\t\tWhen you feel ready to gain more knowledge of the current states of your being, press any button, {userName}. . .");
                        Console.ReadKey();
                        TarotRead(4, "Lords", randomCard);
                        innerLoop = false;
                    }
                    else if (yesOrNo == 'n' || yesOrNo == 'N')
                    {
                        Console.Clear();
                        Type($"\n\n\t\tAn opportunity missed, I am sure, {userName}. The lords will return to their lands.");
                        Thread.Sleep(3000);
                        innerLoop = false;
                    }
                    else
                    {
                        Console.Clear();
                        Type("\n\n\t\t...Yes or No, traveler.\n\t\t[Y/N]");
                    }
                }
            }
        }


        static void TarotRead(int numOfCards, string readingType, Random randomCard)
        {

            int[] pickedCards = new int[numOfCards];
            string[] cards =
            [
                "The Fool",
            "The Magician",
            "The High Priestess",
            "The Empress",
            "The Emperor",
            "The Hierophant",
            "The Lovers",
            "The Chariot",
            "Strength",
            "The Hermit",
            "The Wheel Of Fortune",
            "Justice",
            "The Hanged Man",
            "Death",
            "Temperance",
            "The Devil",
            "The Tower",
            "The Star",
            "The Moon",
            "The Sun",
            "Judegment",
            "The World",
            "Ace Of Wands",
            "2 Of Wands",
            "3 Of Wands",
            "4 Of Wands",
            "5 Of Wands",
            "6 Of Wands",
            "7 Of Wands",
            "8 Of Wands",
            "9 Of Wands",
            "10 Of Wands",
            "Page Of Wands",
            "Knight Of Wands",
            "Queen Of Wands",
            "King Of Wands",
            "Ace Of Pentacles",
            "2 Of Pentacles",
            "3 Of Pentacles",
            "4 Of Pentacles",
            "5 Of Pentacles",
            "6 Of Pentacles",
            "7 Of Pentacles",
            "8 Of Pentacles",
            "9 Of Pentacles",
            "10 Of Pentacles",
            "Page Of Pentacles",
            "Knight Of Pentacles",
            "Queen Of Pentacles",
            "King Of Pentacles",
            "Ace Of Swords",
            "2 Of Swords",
            "3 Of Swords",
            "4 Of Swords",
            "5 Of Swords",
            "6 Of Swords",
            "7 Of Swords",
            "8 Of Swords",
            "9 Of Swords",
            "10 Of Swords",
            "Page Of Swords",
            "Knight Of Swords",
            "Queen Of Swords",
            "King Of Swords",
            "Ace Of Cups",
            "2 Of Cups",
            "3 Of Cups",
            "4 Of Cups",
            "5 Of Cups",
            "6 Of Cups",
            "7 Of Cups",
            "8 Of Cups",
            "9 Of Cups",
            "10 Of Cups",
            "Page Of Cups",
            "Knight Of Cups",
            "Queen Of Cups",
            "King Of Cups",
        ];

            for (int i = 0; i < pickedCards.Length; i++)
            {
                Random random = new Random();
                int pulledCard = random.Next(1, 79);

                if (pickedCards.Contains(pulledCard))
                {
                    if (pulledCard == 78)
                        pulledCard = 1;

                    while (pickedCards.Contains(pulledCard))
                    {
                        pulledCard++;
                    }

                    pickedCards[i] = pulledCard;
                }
                else
                    pickedCards[i] = pulledCard;
            }

            switch (readingType)
            {
                case "Wanderer":
                    Console.Clear();
                    Type($"\n\n\t\tThe wanderer takes your hand. Between your palms, you find the card: {cards[pickedCards[0]]}");
                    CardMeanings(pickedCards[0], randomCard);
                    Type("\n\n\n\n\t\tLet this illumination seep into you, and when you are ready to move on, press any button. . .");
                    Console.ReadKey();
                    break;

                case "Nuns":
                    Console.Clear();
                    Type($"\n\n\t\tThe nun of Warnings offers the card: {cards[pickedCards[0]]}");
                    CardMeanings(pickedCards[0], randomCard);
                    Type($"\n\n\t\tThe nun of Encourage bids you the card: {cards[pickedCards[1]]}");
                    CardMeanings(pickedCards[1], randomCard);
                    Type("\n\n\n\n\t\tLet these illuminations seep into you, and when you are ready to move on, press any button. . .");
                    Console.ReadKey();
                    break;

                case "Spies":
                    Console.Clear();
                    Type($"\n\n\t\tThe spy of the past found the card: {cards[pickedCards[0]]}");
                    CardMeanings(pickedCards[0], randomCard);
                    Type($"\n\n\t\tThe spy of the present offers the card: {cards[pickedCards[1]]}");
                    CardMeanings(pickedCards[1], randomCard);
                    Type($"\n\n\t\tThe spy of the future, shall give the card: {cards[pickedCards[2]]}");
                    CardMeanings(pickedCards[2], randomCard);
                    Type("\n\n\n\n\t\tLet these illuminations seep into you, and when you are ready to move on, press any button. . .");
                    Console.ReadKey();
                    break;

                case "Lords":
                    Console.Clear();
                    Type($"\n\n\t\tThe first lord, the lord of Mind, approaches and gives us the card: {cards[pickedCards[0]]}");
                    CardMeanings(pickedCards[0], randomCard);
                    Type($"\n\n\t\tThe second lord, the lord of Body, follows with the card: {cards[pickedCards[1]]}");
                    CardMeanings(pickedCards[1], randomCard);
                    Type($"\n\n\t\tThe third lord, the lord of Spirit, lays down the card: {cards[pickedCards[2]]}");
                    CardMeanings(pickedCards[2], randomCard);
                    Type($"\n\n\t\tThe fourth and last lord, the lord of Subconsciousness, concluced it with the card: {cards[pickedCards[3]]}");
                    CardMeanings(pickedCards[3], randomCard);
                    Type("\n\n\n\n\t\tLet these illuminations seep into you, and when you are ready to move on, press any button. . .");
                    Console.ReadKey();
                    break;
            }
        }

        static void CardMeanings(int num, Random random)
        {
            num += 1; // Holy spaghetti code to avoid changing the switch case to 0-index, LMAO.
            int upOrRev;
            switch (num)
            {
                case 1:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Fool shies away from you, keeping all that is good with his coming away from you. You might have taken a bad decision, or you are indesicive, apathetic, hesistant and faulty.");
                        Type("\n\t\t\tNew beginnings might be around the corners... beginnings you might not love.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Fool is laughing at you. He symbolizes new beginnings, adventures, and opportunities. He brings pleasure, passion, thoughtlessness and rashness.");
                        Type("\n\t\t\tAre you daydreaming much, my child? It is time to wake up and seize your opportunity, or fall, just like The Fool.");
                    }
                    break;

                case 2:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Magician scoffs at you. You are currently weak of will, and you reek of insecurity, delay and lack of imagination.");
                        Type("\n\t\t\tMajor opportunities might be on your horizon, do NOT let them pass you by. Watch out for snakes calling themselves your friends. Embrace opportunity, release your weakness.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Magician stares you down, albeit with respect. His magic offers originality, creativity, skill, self-confidence and dexterity.");
                        Type("\n\t\t\tHave you been having an idea lately, wondering if it is good or not? Maybe a solution to a problem, that could solve your career, love life or current endeavor? Trust that it is a GOOD idea, child.");
                    }
                    break;

                case 3:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe High Priestess remains silent. Her action symbolizes ignorance, lack of understanding, selfishness, shallowness. Are you currently ignoring your intuition?");
                        Type("\n\t\t\tIf so, stop it, now. Seek out human connection, and stop ignoring your desires that are only human in nature.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe High Priestess offers you a smile. She radiates with wisdom, knowledge, learning, intuition, purity, virtue, lack of patience and a will to teach you.");
                        Type("\n\t\t\tYou might have to give yourself time to learn something, so that you can make a wise decision. You should pay some time to truly listen to your dreams and their signs. They are worth your attention.");
                    }
                    break;

                case 4:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Empress looks down at you whilst she clicks her tongue. Her action symbolizes inaction, lack of concentration, indecision, anxiety and infidelity. You are not giving your feminine side enough attention.");
                        Type("\n\t\t\tThere will be consequences for this. Have you been neglecting your spiritual work? Are you lacking confidence? Try and straighten your back, my child.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Empress bows her head towards you, showing grace. She offers action, development, accomplishment, sisterhood and evolution. Are you longing for something? Practice patience, and let your femininity blossom.");
                        Type("\n\t\t\tA child might be coming your way. Be a good role model, the Empress asks of you.");
                    }
                    break;

                case 5:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Emperor flicks his hand, dismissing you from his court. He sees your immaturity, indecision and petty emotions. You lack strength and stability. The ground underneath you might be shaky currently.");
                        Type("\n\t\t\tIs there an older male in your vicinity that is causing you distress? Is he making you feel powerless? You need to stand up to authorities around you, either loudly or quietly. Do not forget, YOU are the ruler of your fate.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Emperor does not move a muscle, yet gives you a feeling of acceptance. He offers you power through agression, courage and boldness. Do you have a demanding person of authority close to you?");
                        Type("\n\t\t\tMaybe this leader is not demanding, but charismatic. It is up to you to decide - use your power to act accordingly. Do NOT accept disrespect.");
                    }
                    break;

                case 6:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Hierophant shouts in cursed tongues at you! You are unconventional and unorthodox, vulnerable and frail. You have a desire to mix things up.");
                        Type("\n\t\t\tFreedom is waiting for you, but it will come with the cost of casting norms aside. The alternative path is open for you.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Hierophant murmurs a blessing for you. He sees your need to conform (social approval), and simultaneously sheds light on your mercy, forgiveness, compassion and inspiration.");
                        Type("\n\t\t\tYou know how to solve your problems, dear. It might not be easy, but not all things great are so.");
                    }
                    break;

                case 7:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe lovers catches you peeping, and shouts for you to leave. You ooze of unreliability, separation and frustration in love. You are untrustworthy right now.");
                        Type("\n\t\t\tIf you\'ve recently made a decision, it might be something you second-guess. It was YOUR decision. You have to reap what you sow.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe lovers spots you, and opens their sheets with giggles and smirks to invite you in. They sense your power in love, harmony, trust and honor. You are passionate, and they sense that too.");
                        Type("\n\t\t\tA divergence might come your way, do not dare to miss it, friend. No matter how difficult it might be, seek it! The easy road might not be righ for you. Take the leap of faith.");
                    }
                    break;

                case 8:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Chariot tosses you off. Defeat is in the air, as is failure and the feeling of being vanquished. You are NOT in control right now.");
                        Type("\n\t\t\tReassess your wants and desires. Are your goals truly in line with your best interests? Time to reflect on this.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Chariot stands ready for you. They give off perserverance, a journey, rushed decision, adversity and vengeance. They are urging the uniting of opposites.");
                        Type("\n\t\t\tThis is your call to take action in a stagnant situation. Victory is coming, you are in control. Trust.");
                    }
                    break;

                case 9:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tStrength leaves you, making you feel small and weak. You are petty, or sick, lacking faith and might be abusing powers. You are brave, but you do not use it.");
                        Type("\n\t\t\tIt is time to call Strength back. Drop your insecurities and let go of low self-esteem. It is time to fight.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tStrength enters you. With it, it brings courage, conviction, energy, determination and action. You are in control, of your situation and yourself.");
                        Type("\n\t\t\tAre you riddled with any doubts, my child? Find your inner strength. With it, you can move mountains.");
                    }
                    break;

                case 10:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Hermit throws rock at you from his hiding spot. He accuses you of imprudence, hastiness, rashness and being immature. Have you isolated yourself lately? Stop it if you care for yourself.");
                        Type("\n\t\t\tEven if time has been spent alone, you have not spent it well. You are avoiding to deal with your real problems, both internally and externally.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Hermit accepts you into his cave. There you will find counsel, inner strength, prudence, caution, vigilance, patience, and withdrawal.");
                        Type("\n\t\t\tYou might be wanting to be alone for now. Others might not see or understand what you do. give yourself space to self-reflect.");
                    }
                    break;

                case 11:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Wheel of Fortune breaks down as you spin it. Failure is spun, so is bad luck and unexpected loss. This is your own fault, take responsibility.");
                        Type("\n\t\t\tYou took unnecessary risks. It is time to accept the inevitable with some dignity - bad luck will not stay with you for eternity.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Wheel of Fortune spins in your favor! Luck is with you. The Universe is guiding you to fortunate experiences. Take advantage, go to where you mostly wish.");
                    }
                    break;

                case 12:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe scales of Justice are not balances. You are being biased, might claim false accusations. Are you perhaps intolerant, unfair and abusive right now..?");
                        Type("\n\t\t\tMaybe you are the victim of these feelings. Justice must be restored. Do what you can to restore it. Right the situation.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tJustice is with you. There is harmony, balance, equality, righteousness and honor in the air. You feel virtuous and accepting of advice. This is a time for adjustments.");
                        Type("\n\t\t\tThis is a time to bring back physical, emotional, social and spiritual things into balance. Stand tall and strong against anything - or anyone - that might throw you off balance!");
                    }
                    break;

                case 13:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Hanged man seems to be struggling. You sense unwillingness to make an effort. False prophecy is in the air, as is useless sacrifice.");
                        Type("\n\t\t\tYou are not as happy as you can be, and you are making bad desicions to not deal with it. Take a step back, breathe, give yourself some time to figure out the next step, and by the goddess, fix your attitude problem.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe face of the Hanged Man is peaceful. He gives you the feelings of suspension, change, reversal, sacrifice, readjustment, improvement, REBIRTH.");
                        Type("\n\t\t\tStop resisting, dearest. Reflect, move on. It is time to sail forward.");
                    }
                    break;

                case 14:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tDeath approaches, and you fill up with dread. \"Thou have cheated me\". Change is approaching, and you are resisting it. This is bad.");
                        Type("\n\t\t\tLet go and move forward. Take aim for a brighter future.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tDeath is present, but you are not afraid. Transformation is here, and with it the New, loss, failure, illness or death and bad luck. Do not fret! All of this might be the end of a chapter, and the beginning of a new one.");
                        Type("\n\t\t\tThis is a time of transition. Embrace it, and the pain. You are ready.");
                    }
                    break;

                case 15:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe cups of Temperance are leaking from their cracks. Conflict is seeping out, and with it, disunion, hostility, frustration and impatiance.");
                        Type("\n\t\t\tYou are being reckless. You are overindulging. You might be out of sync with the ones closest to you. Beware of this.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe cups of Temperance are more than half full. They are filled with moderation, temperance, patience, harmony, fusion, good influence and confidence. As you drink, you sense a warning to temper your behavior.");
                        Type("\n\t\t\tYou are connected with what makes a good life. Your moral code remains strict, and you toil hard to reach your goals. This is good.");
                    }
                    break;

                case 16:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Devil appears, yet you do not cower. He brings with him release, enlightenment, overcoming handicaps, divorce. It is time to let go, and move on.");
                        Type("\n\t\t\tHe reminds you of what has held you back in the past. Curse it, and move on!");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Devil appears, mockingly laughing. Downfall, failure, violence, disaster is here for you.");
                        Type("\n\t\t\tHe warns you to not restrain yourself. Be passionate, be ambitious. Find your inner devil. You are the one that put shackles unto yourself.");
                    }
                    break;

                case 17:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Tower stands firm. You might be trapped within it, in a bad situation. Shake things up - I know you have been procrastinating this.");
                        Type("\n\t\t\tQuestion the traditions and habits you have been following blindly. What can you throw away in order to make room for something greater?");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe Tower crumbles as you near it. Sudden, massive change is about to happen. Abandon the past, child, before it is too late.");
                        Type("\n\t\t\tYou might lose money, security or short term happiness... but in the catastrophe, you can build something much stronger than you ever had before.");
                    }
                    break;

                case 18:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe stars barely twinkle in a dark sky. Crushed dreams, unfulfilled hopes and back luck fills your lungs.");
                        Type("\n\t\t\tDespite this, cling to hope, friend. Things CAN turn around. You can do this.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe stars shine brightly. Hope, faith, inspiration, optimism, insight, spiritual love, pleasure and balance radiates from it.");
                        Type("\n\t\t\tYou have worked through hardships, you are ready for whatever may come. Stay calm, stay positive. Unexpected help might wait for you, but it is in your hands to find your way.");
                        Type("\n\t\t\tThis is very good, indeed. Your possibilities are endless, and you are ready to grab it all. You have earned this.");
                    }
                    break;

                case 19:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tA new moon rises. You are blessed to be able to discover deception before it falls upon you. You might also be able to take advantage of someone.");
                        Type("\n\t\t\tYou can release your negative energy and let go of your fears. You shall emerge with newfound confidence and clarity.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe moon shines fully tonight. Deception, trickery, disillusionment, error, anxiety, danger and double-dealing hangs in the air.");
                        Type("\n\t\t\tThis might be a time of emotional or mental trials. Pay attention to your dreams, they are trying to tell you something.");
                    }
                    break;

                case 20:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe Sun is being covered by cloudy skies. She is unhappy with you. You feel that doubt is close to mind, an uncertain future with canceled plans.");
                        Type("\n\t\t\tYou might have been overstepping lately. You might feel lonely, that your friends are shutting you out. Put some more energy into it. Push through these clouds.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe glorious Sun warms your skin. You can feel satisfied, accomplished, success and joy. Feel free to step out in the light.");
                        Type("\n\t\t\tThings are going to work out just fine. People around you will benefit from your warmth, as will you.");
                    }
                    break;

                case 21:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tYou are being judged. Fear of death is present, or failure, possible loss and ill health. You might be turning away from a challenge, and you will be punished for it.");
                        Type("\n\t\t\tYour mind is filled with doubt. You do not believe you are capable.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tJudgement is on your side. Awakening, renewal, a well-lived life wih better health and quickened mind is your sentence.");
                        Type("\n\t\t\tDo not rest yet, however. You might have a challenge in need to face head on. You are ready to be judged, stop being held back and step into the future.");
                    }
                    break;

                case 22:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe world is revealed as burning, failing. Imprefection, flawed vision and disappointment will follow.");
                        Type("\n\t\t\tYou might be wanting to take shortcuts, but that will only lead to more frustration, and the result will be... disappointing.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe World is revealed to your inner eye. It looks complete, perfect. She gives you recognition, addition to honors, the end result, success, fulfillment, triumph and eternal life.");
                        Type("\n\t\t\tShe offers you a happy end to a project, that will come with well-earned praise, celebration and success. Well done, sweet child.");
                    }
                    break;

                case 23:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You have creative potential, it is just not fully manifested. The ideas are big, but what form will they take?");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. It is time to be daring and brave, to take the risks in order to get what you desire!");
                        Type("\n\t\t\tChoose the way that excites you and move! Push beyond limits. Trust your potential, be original if possible. There are no limits.");
                    }
                    break;

                case 24:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are being held back. Did you not plan ahead, you fool? You might not be moving forward, and not taking risks.");

                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. The 2 of Wands lends you courage to be great. Do not think that the power is yours, though. You are but a conduit, the Goddess is the source of the power.");
                        Type("\n\t\t\tLet these powers be used in a positive manner. You are free to try new approaches. Be bold and take initiative!");

                    }
                    break;

                case 25:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You might be lacking progress, and it might be turn to go home.");
                        Type("\n\t\t\tYou seem to not be able to see the bigger picture, and this might leave you unhappy.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. The 3 of Wands represents vision. With it, you might gain perspective. Do not act in the heat of the moment.");
                        Type("\n\t\t\tTake a step back and reconsider your actions. Look at the greater picture and dare to dream. Be fearless and explore new territory.");
                    }
                    break;

                case 26:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. This represents discontented home life and canceling of good times.");
                        Type("\n\t\t\tYou are taking things way too personally. This might lead to a disconnection with home, and with friends.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. This means the potential breaking of physical, emotional or mental bonds. A new era of personal growth is ahead of you.");
                        Type("\n\t\t\tThis is a time to be excited for! A period of growth and joy is upon you. Break your shackles, be free.");
                    }
                    break;

                case 27:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. The fighting is ending. Conflicts are resolving as we speak, since everyone involved is ready to move on.");
                        Type("\n\t\t\tYou might feel exhausted with the fight. You might not have gotten what you wished for, but you are done nonetheless.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You might feel that everything is against you. This card represent the thorn in your side, the pebble in your boots.");
                        Type("\n\t\t\tIt demands that extra bit of patience and perserverance to accomplish things. Your ride is a bumpy one. Beware of rivals, opposing you for position.");
                    }
                    break;

                case 28:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are not living the dream currently. You might be following someone else, like a blind sheep. That person might be arrogant or grandiose.");
                        Type("\n\t\t\tBeware, beware.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Achieving your goal is within reach. What you deserve will come to you. This victory might be over yourself.");
                        Type("\n\t\t\tBeware of pride. Do not become arrogant with this win. It might leave you tomorrow.");
                    }
                    break;

                case 29:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are turning your back on your own truths.");
                        Type("\n\t\t\tYou might be compromising in this moment. You might be unpopular. Watch yourself closely.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You might be involved in conflict. Is this truly one worth fighting? If so, defend with boldness and aggression. Take a stand.");
                        Type("\n\t\t\tTo do this, you must believe in yourself. If you do not, give up your position.");
                    }
                    break;

                case 30:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Are you not acting? You might be stuck in a rut, either mentally or physically... reflect on this.");
                        Type("\n\t\t\t");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Everything will hopefully go right for you, if you act quickly. Stay vigilant, good news might be on the horizon.");
                        Type("\n\t\t\tThis is not the time for hesitation. Act quickly! Things of old might be over, and new eras might take their place.");
                    }
                    break;

                case 31:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Someone else in your life is currently being incredibly stubborn. If not, you might feel physical or mental weakness.");
                        Type("\n\t\t\tMaybe you are even fretting over past actions. Maybe it is time to let your stand go and compromise.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Proceed with caution, or risk being hurt. Do not let bitterness overtake you. Stay watchfull.");
                        Type("\n\t\t\tDo not give up, whatever the opposition! Despite setbacks, you have resisted. Keep doing so.");
                    }
                    break;

                case 32:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed.");
                        Type("\n\t\t\tYou are feeling the weight of your responsibilities. If you keep pushing like this, you will break.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. This card signifies that you might have pushed yourself too hard. Let yourself rest, child.");
                        Type("\n\t\t\tCan you share your responsibilities with someone else? You need to find balance in your life.");
                    }
                    break;

                case 33:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Setbacks and delays might be on its way for you. You are not following through on your goals.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. An opening is imminent, an opening that will excite you. It is time to take a novel approach to things.");
                        Type("\n\t\t\tDo not fear to take a risk! Be daring and overcome your fears. The time of acting is now.");
                    }
                    break;

                case 34:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You might have felt setbacks recently, and are not sure how to overcome them.");
                        Type("\n\t\t\tThey are probably your own fault. You might have been overly confident... but it could also be due to an arrogant man.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Are you feeling confident right now, or cocky? Adventure might be the way to go right now.");
                        Type("\n\t\t\tThe time of having fun might be upon you, as long as it is confidence that surges within you.");
                    }
                    break;

                case 35:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You might currently be overwhelmed...");
                        Type("\n\t\t\t... maybe due to an overbearing woman.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Time to shake off your bad feelings! No more low energies, time to be gung-ho for a while.");
                        Type("\n\t\t\tCreate space for cheerfulness and confident enthusiasm.");
                    }
                    break;

                case 36:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Are you feeling lethargic? Unmotivated perhaps?");
                        Type("\n\t\t\tMaybe a rude and unhelpful man is holding you back...");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. It is time to act like a king. Take risks, lead the way, attract attention from subjects.");
                        Type("\n\t\t\tCreate a masterful kingdom!");
                    }
                    break;

                case 37:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. A chance might have been missed, an opportunity lost.");
                        Type("\n\t\t\tThis might be due to lack of confidence... whatever it is, you are missing chances. How sad.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You must focus now, child, on being practical. Seek reliable experiences and build trust in your life.");
                        Type("\n\t\t\tLet dreams relax for a bit, be in the \"real\" world.");
                    }
                    break;

                case 38:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You have taken on more than you can handle. The pressure is forcing you to making bad decisions.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You have to believe in yourself. You can handle the demands put on you for now.");
                        Type("\n\t\t\tYou might not feel confident right now. Regardless, you can handle the challenges. Believe and trust in that!");
                    }
                    break;

                case 39:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are not learning from your mistakes. You will not move forward if you do not learn from them.");
                        Type("\n\t\t\tGrowth takes effort, my friend.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. This is not the time to rush into things. Plan and prepare.");
                        Type("\n\t\t\tTime to do your homework. See the details and plan accordingly.");
                    }
                    break;

                case 40:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are ready to let go. Release your connections to toxicity, let go of the past.");
                        Type("\n\t\t\tIf you currently feel generous, do not let it go too far. People might wanna take advantage of you.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are being too controlling. Stop trying to know every move of people around you.");
                        Type("\n\t\t\tFull control is impossible. You can only determine your own path.");
                    }
                    break;

                case 41:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. This is a good sign. Some hardship might finally be over, or your physical health might be improving.");
                        Type("\n\t\t\tSomething good is coming your way. Cut ties with toxicity and see yourself blossom.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is reversed. This could mean many things. Maybe bad times are upon you. Maybe you are not taking care of your physical health.");
                        Type("\n\t\t\tTime to prioritize health and welness. If someone around you offers help, take it. These bad times will pass one day.");
                    }
                    break;

                case 42:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. This represents stinginess. Beware of generosity from people around you, it might have strings attached to it.");
                        Type("\n\t\t\tMaybe YOU are the generous person. This means that you are abusing it. Stop it as soon as you can.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Fortunes can change, and quickly. Time for you to reconsider.");
                    }
                    break;

                case 43:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Your current work has not shown fruit. You might not be working hard enough.");
                        Type("\n\t\t\tIf this does not resonate, maybe you have been taken on too much work. Regardless, you are not progressing.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Time to catch your breath, traveler. Reflect on what you have accomplished, be sure that it is the results that you want.");
                    }
                    break;

                case 44:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are lazy, negligent, not focused... or maybe the complete opposite, and you are overdoing things.");
                        Type("\n\t\t\tYou must sit down, breathe, and look over what is worth to prioritize. You might not be putting your energy in the right place right now.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Time to give your project your all, whether it is professional or personal.");
                        Type("\n\t\t\tCorrect errors, put it all in that one basket. This will surely lead to success.");
                    }
                    break;

                case 45:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are not free. This is your reminder that the best things in life are, indeed, free.");
                        Type("\n\t\t\tMaterialism will not make you content. Stop overindulging, switch focus to other areas.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Let work rest for a bit. Give your time to the finer things in life for a while.");
                        Type("\n\t\t\tDuring this time, trust your own abilities. Take matters into your own hands.");
                    }
                    break;

                case 46:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Things are unstable as we speak. Unexpected change is here.");
                        Type("\n\t\t\tAvoid doing anything illegal for a while, but still dare to go against tradition.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Now is the time to stick to convention. Avoid changes if you can. Stick to the well walked path.");
                    }
                    break;

                case 47:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Bad things are going to happen, and it is entirely your own fault.");
                        Type("\n\t\t\tYou must act. Stop procrastinating, and fight!");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Opportunities are on the rise, but only if you act on them. Let go of those daydreams, channel some common sense.");
                        Type("\n\t\t\tHave faith in those around you, they might come in handy. Show that you yourself is dependable to have around. Seize the glory!");
                    }
                    break;

                case 48:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are being a brat right now, wishing for rewards without putting in the work. Your dreams will never materialize, if you do not work for them.");
                        Type("\n\t\t\tStop being so pessimistic and close-minded. It is not working out for you.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You might be working very hard right now... maybe even too hard. Are you listening to reason when she voices her worries?");
                        Type("\n\t\t\tYou might be unbalanced. Make changes to find your footing.");
                    }
                    break;

                case 49:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Consider this a warning. You, or someone close to you, are not acting in an upstanding manner.");
                        Type("\n\t\t\tOne of you are either overly materialistic, deeply insecure or generally untrustworthy right now.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are currently feeling warmth and caring towards others. You are being sensible and staying true to your word.");
                        Type("\n\t\t\tYou can be trusted right now. This is very good, indeed.");
                    }
                    break;

                case 50:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Right now, your goals are not being reached. You are either lazy, or these goals are not the right ones for you.");
                        Type("\n\t\t\tReassess, and make a practical plan on how to move forward.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Time for you to be a king. Be steadfast, keep your commitments to others and not least yourself.");
                        Type("\n\t\t\tYou are the backbone. Make it all happen with a little hard work, and a ton of reliability.");
                    }
                    break;

                case 51:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are having a hard time seeing the way forward. Take your time to work through your issue.");
                        Type("\n\t\t\tTry and objectively think about your problems. You can get through this.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. A Sudden burst of insight might hit you. It is time to face your challenges with courage and honesty.");
                        Type("\n\t\t\tLet this be a reminder that you have the inner strength to overcome your challenges.");
                    }
                    break;

                case 52:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are being indecisive. There might even be fear and confusion involved.");
                        Type("\n\t\t\tAre you unable to act? Perhaps... or you might actually, finally see things for what they are.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Stop and truly think about what you are feeling. Are you perhaps resisting? Afraid of getting hurt?");
                        Type("\n\t\t\tIs your inner being filled with anger, but you only show a smile to the world? Putting up these barriers will NOT help you. Face the truth, acknowledge the isseu at hand.");
                    }
                    break;

                case 53:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Maybe this is the time for you to let go of pain, and move past sadness. You can take this opportunity to forgive and move on.");
                        Type("\n\t\t\tDo not be too hard on yourself, child. It will not help you.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Please, beware: your world is about to be turned upside down.");
                        Type("\n\t\t\tSomeone in your inner circle is stabbing you in the back. Might be a lover cheating on you, or a best friend talking ill about you.");
                        Type("\n\t\t\tTake nothing for granted. Try and head off trouble. Do not try and hurt or betray anyone.");
                    }
                    break;

                case 54:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are finanly finding strength to get through something. You are ready to address stress and anxiety.");
                        Type("\n\t\t\tTake care of yourself. It has been some time since you looked after yourself.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Pause, and contemplate. Put things into the correct perspective. Take some time to prepare quietly.");
                        Type("\n\t\t\tYou might be completely healthy, but this is still time to take a break.");
                    }
                    break;

                case 55:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. A solution might be on the way. Peace, harmony, understanding, and a way to finally move on.");
                        Type("\n\t\t\tIf you do not find these, this card might tell you that some actions in your mind are risky. Someone needs to be held accountabe.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. If someone is taking advantage of you, now is the time to break free. Claim your rights, the rights to your own time and life.");
                        Type("\n\t\t\tThere is a need to understand the Self, and to stop defining it so narrowly.");
                    }
                    break;

                case 56:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are pushing against change, clinging to what is left. This keeps you stuck in a rut.");
                        Type("\n\t\t\tIf this keeps going, you will not be able to grow.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You might have been in rough times lately. This is your sign to recover. Fights might have left you numb and not caring.");
                        Type("\n\t\t\tYou might need to move. Discover new places, whether that is in the physical space or an inner journey.");
                    }
                    break;

                case 57:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You might feel like a fake, or a fraud, even though you are not one.");
                        Type("\n\t\t\tYou or someone close to you might be harboring secrets, or being deceptive. Beware!");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You or someone close to you wants to be alone. A need for independence and standing on their own legs can be felt.");
                        Type("\n\t\t\tThis might be a good choice, but it could also mean running away from commitments or responsibilities. You cannot wish your problems away, friend.");
                    }
                    break;

                case 58:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Freedom fills this reading. You will finally break free and fly from whatever has weighed you down.");
                        Type("\n\t\t\tYou will take over the world. Believe.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Are you currently finding yourself lost and confused? You might not even have an idea how you ended up in your situation.");
                        Type("\n\t\t\tSolutions to this are not easily found, but they do exist, all around you. Try and center on clarity of thought, and move from there.");
                    }
                    break;

                case 59:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. This might be the time to let go of fear. To give yourself space to recover, and then to start working towards a greater life.");
                        Type("\n\t\t\t...But it could also be a time of major emotional turmoil. Try and face it without fear.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. A great shadow of worry is hanging over you. It is torturing you. Distress might also be a part of your existence right now.");
                        Type("\n\t\t\tThis is not a fun card to pull in a reading... I warn you that times are going to be difficult. Hard choices might appear soon.");
                    }
                    break;

                case 60:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Painful events might have passed. This is the time to pick yourself up, dust yourself off, and get on with it.");
                        Type("\n\t\t\tIf this does not resonate with you, this card might mean difficulty from your past is coming back into your life.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are currently wearing a victim personality... and you are enjoying it.");
                        Type("\n\t\t\tStop being this pathetic, child. Check your attitude, things are about to pick up.");
                    }
                    break;

                case 61:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Your ideas are lacking inspiration. You do not really know how to express them at this point, and it shows.");
                        Type("\n\t\t\tYou might also have gifts, but are using them for the wrong reasons...");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. It asks you to accept the difficult circumstances thrown at you. They are here to test your mettle.");
                        Type("\n\t\t\tAccepting and defeating them will make you stronger. Stay honest, reasonable, show integrity and fortitude. We believe in you.");
                    }
                    break;

                case 62:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThis card is reversed. Your heart might be ruling over your head, as we speak. Her grip makes you afraid to speak your mind.");
                        Type("\n\t\t\tA big opportunity might be heading your way, and you are not properly prepared for it... but you should seize it anyway. Stop being a follower, become a leader.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You might be forcing your opinions on others. Have you noticed if speaking your mind gets you into trouble?");
                        Type("\n\t\t\tIf yes, maybe it is time to try and change some of these traits.");
                    }
                    break;

                case 63:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You, or someone real close to you, is being overly emotional.");
                        Type("\n\t\t\tIf this is not you, it might be coming from a distant older woman in your life. Cruel, closed off, easily susceptible comes to mind.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. It is calling you to be completely honest, with yourself and people close to you. Make sure you are not being fooled by those around you.");
                    }
                    break;

                case 64:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are feeling a hunger for power. This might you become tyrannical and out of control.");
                        Type("\n\t\t\tYou are currently being too aggressive, oppressive and judgmental. Check yourself.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. The king of this suit is urging you to act like he would. Think up solutions to issues. Communicate well.");
                        Type("\n\t\t\tJudge fairly, stay truthful. This will serve you.");
                    }
                    break;

                case 65:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You might not be able to express your love so much at this moment.");
                        Type("\n\t\t\tTime to give yourself some \"me time\"");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are a vessel of love. Do not think this is isolated to romantic love, however.");
                        Type("\n\t\t\tIf needed, ask for forgiveness. Set your anger aside, look for peace.");
                    }
                    break;

                case 66:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Disharmony is upon you. This would mostly mean a romantic relationship, but is not isolated to it.");
                        Type("\n\t\t\tYour relationship might be a one-sided one. The lack of harmony is weighing on you.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Look for one-on-one connections. Work on partnerships, both romantic and platonic.");
                        Type("\n\t\t\tHarmony is upon you.");
                    }
                    break;

                case 67:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Social events are being cancelled, engagements are coming to end. People around you might be jealous of you instead of supportive.");
                        Type("\n\t\t\tBe careful of who you trust. Everything is not as it seems at first glance.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Celebrations and parties are taking up your time and near future.");
                        Type("\n\t\t\tThis is the time to offer support to those you love, and accept it from them in return.");
                    }
                    break;

                case 68:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are ready to fight your way out of a rut. You shall leave the bad feelings being you.");
                        Type("\n\t\t\tA life of light is awaiting, without remorse or regret. This is a blessing you are worthy of.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are stuck in a rut, focusing on things and actions that make you filled with apathy.");
                        Type("\n\t\t\tNo one can free you from this, except for yourself. Focus on things that are good for you. Open up. Get out!");
                    }
                    break;

                case 69:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are ready to finally heal. Grief will leave you shortly, and the other side is waiting for you,");
                        Type("\n\t\t\tto fill you with acceptance and newfound ability to move on.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. There might be loss heading your way. You might even beware of it already.");
                        Type("\n\t\t\tYour mission should be to reduce the importance of it. Do not get swallowed by grief.");
                    }
                    break;

                case 70:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. It is time to move on from your past. You are ready to work through some childhood trauma,");
                        Type("\n\t\t\tor simply let the past go. A fresh start is what you need right now. Time to be an adult, darling.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. You are currently focused on the path. Probably been thinking about an old friend of late.");
                        Type("\n\t\t\tIf you have been struggling lately, it is a good time to seek and find support in loved ones.");
                    }
                    break;

                case 71:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Take this as a wake-up call. You might have been living in a dream for too long. This is the time to make up your mind");
                        Type("\n\t\t\tand act with decisivness. You might be running out of options. Choose one that feels right and go with it.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. There might be too much rigidity around you right now. Try and loosen up. Letting go of control could reward you");
                        Type("\n\t\t\twith creativity. However, if you feel chaos around you, let this be a call to grab control. Find balance between order and disorder.");
                    }
                    break;

                case 72:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are afraid of change. You are in a bad place, and are not brave enough to move on.");
                        Type("\n\t\t\tThe fear is forbidding you to get to a much better place in life. Let go of fear, she is but a cruel mistress right now.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Realize that the past is gone. The end is nigh, and what you thought to be permanent is coming to a close.");
                        Type("\n\t\t\tThe change is staring you right in the face. Please, do not look away. Move on.");
                    }
                    break;

                case 73:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. This is a card of loss. Something that you thought you always wanted is turning out to be wrong for you, or maybe you just lost it.");
                        Type("\n\t\t\tYour lack of self-esteem and bad feelings about yourself are preventing you from achieving what you would be able to grasp.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Seek and enjoy pleasures of all kinds. These are days and nights of pure indulgence.");
                        Type("\n\t\t\tHowever, this does not mean to indulge at the expense of someone else. Care for the consequences, and accept responsibility.");
                    }
                    break;

                case 74:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are seeking for a deeper connection in a relationship, but you are trying too hard. You are being unrealistic.");
                        Type("\n\t\t\tStep away. Reasess, find reality in your desires.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. All elements are in harmony around you. If you despite this feel disharmony,");
                        Type("\n\t\t\tthis is a sign that it will subside. Seek closeness in family!");
                    }
                    break;

                case 75:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Insecurity looms around you. Jealousy is within you. You are losing touch with things and");
                        Type("\n\t\t\tpeople that bring you joy, especially with roots in your childhood.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Joy is coming into your life! Look out for good news on the horizon.");
                        Type("\n\t\t\tLet childlike happiness fill your lungs and heart. Let it make you wild for a while.");
                    }
                    break;

                case 76:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. Sadness, heartbreak, broken resolutions are heading in your direction. You need to slow down, my friend.");
                        Type("\n\t\t\tDo not act hastily, it will only lead to more sadness right now. Beware of fraudulent men around you as well.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Check your feelings. Are you full of emotions, or ruled by common sense? This is a time for balance.");
                        Type("\n\t\t\tIf you find yourself quiet, try to be more expressive. If the opposite is more resonant, maybe quiet down a bit.");
                    }
                    break;

                case 77:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You must give yourself time and space to focus on your own spirituality and emotions. Tend to your deepest");
                        Type("\n\t\t\tneeds and desires for a while, and let others take care of their issues. The time for self-care is here.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. The Queen of Water urges you to tap into your femininity. Know that the women in your life will have your back.");
                        Type("\n\t\t\tTreat yourself with kindness, compassion and love. You are deserving!");
                    }
                    break;

                case 78:
                    upOrRev = random.Next(1, 101);
                    if (upOrRev % 2 == 0)
                    {
                        Type("\n\n\t\t\tThe card is reversed. You are being immature about things. You are overreacting in certain issues.");
                        Type("\n\t\t\tTake a deep breath and examine where you stand internally.");
                    }
                    else
                    {
                        Type("\n\n\t\t\tThe card is upright. Diplomacy is needed. Try and see issues from other peoples\' perspective.");
                        Type("\n\t\t\tTolerance and understanding should be your guiding values as of now.");
                    }
                    break;
            }
        }
    }
}
