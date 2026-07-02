using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CyberChatbotGUI_st10448877_POE

{
    public static class CyberData

    {
        public static void Initialize()
        {

        }  // All dictionaries 
        public static string GetRandomTip(string topic)
        {
            if (TopicTips.ContainsKey(topic))
            {
                List<string> tips = TopicTips[topic];
                Random rnd = new Random();
                int index = rnd.Next(tips.Count);
                return tips[index];
            }
            return "Sorry, I don't have tips for that topic.";
        }

        public static Dictionary<string, string> Definitions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            //DEFINITON
            { "password", "A password is a secret word or phrase used to gain access to a system. Strong passwords are crucial for protecting your accounts and personal information." },
            { "malware", "Malware refers to software specifically designed to disrupt, damage, or gain unauthorized access to a computer system." },
            { "vpn", "A Virtual Private Network (VPN) helps protect your privacy online by encrypting your internet connection and hiding your IP address." },
            { "firewall", "A firewall is a network security system that monitors and controls incoming and outgoing network traffic based on predetermined security rules." },
            { "phishing", "Phishing is a cyberattack that tricks people into providing sensitive information by pretending to be a trustworthy entity." },
            { "privacy", "Privacy refers to your ability to control your personal information and how it is collected, used, and shared." },
            { "cybersecurity", "Cybersecurity is the practice of protecting systems, networks, and programs from digital attacks." },
            { "antivirus", "Antivirus software is designed to detect, prevent, and remove malicious software from your device." },
            { "social engineering", "Social engineering is the use of deception to manipulate individuals into divulging confidential or personal information." },
            { "scam", "A scam is a fraudulent scheme performed by a dishonest individual or company in an attempt to obtain money or something else of value." },
            { "ransomware", "Ransomware is a type of malware that locks or encrypts a victim's data and demands a ransom to restore access." },
            { "2fa", "Two-factor authentication (2FA) adds an extra layer of security by requiring not only a password and username but also something that only the user has on them." },
            { "spyware", "Spyware is a type of software that secretly gathers information about a person or organization without their knowledge." },
            { "browser safety", "Browser safety involves using secure settings and tools while browsing the internet to prevent data theft, tracking, and malicious attacks." },
            { "public wifi", "Public Wi-Fi networks are often unsecured, making it easy for attackers to intercept your data unless you use protective measures like a VPN." },
            { "purpose", "I’m here to educate you about cybersecurity and help you stay protected in the digital world." },
            { "how are you", "I'm just a bunch of code, but thank you for asking!" },
            { "thanks", "You're welcome! Let me know if there's anything else I can help you with." }
        };

        public static Dictionary<string, string> Sentiments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            //SENTIMENTS
            { "worried", "It’s okay to feel worried. I'm here to help you feel secure." },
            { "frustrated", "Cybersecurity can seem tricky, but you're not alone." },
            { "curious", "Curiosity is great! Ask me anything." }
        };

        public static Dictionary<string, List<string>> TopicTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            //TIPS
            { "password", new List<string> {
                "Use different passwords for each account.",
                "Avoid using names or birthdays in your password.",
                "Use a password manager to store strong passwords.",
                "Change your passwords every 3 to 6 months.",
                "Avoid reusing old passwords.",
                "Include special characters, numbers, and uppercase letters."
            }},
            { "malware", new List<string> {
                "Don’t download files from unknown sources.",
                "Keep your antivirus software updated.",
                "Update your operating system regularly.",
                "Avoid clicking pop-up ads or fake warnings.",
                "Use reputable anti-malware tools.",
                "Disconnect from the internet if you suspect malware."
            }},
            { "vpn", new List<string> {
                "Use a VPN on public Wi-Fi.",
                "Choose a VPN that doesn’t log your data.",
                "A VPN helps protect your location and traffic.",
                "Avoid free VPNs — they may sell your data.",
                "Use a VPN when traveling internationally.",
                "Check for DNS and IP leaks when using a VPN."
            }},
            { "firewall", new List<string> {
                "Keep your firewall turned on.",
                "Firewalls block unwanted incoming traffic.",
                "Use both software and hardware firewalls for safety.",
                "Don’t ignore firewall alerts.",
                "Configure custom rules to block suspicious programs.",
                "Test your firewall using online tools."
            }},
            { "phishing", new List<string> {
                "Don't click links in suspicious emails.",
                "Always double-check sender addresses.",
                "Scam emails often contain typos or urgent language.",
                "Don’t share personal info over email.",
                "Hover over links to preview the real URL.",
                "Banks will never ask you to verify info via email."
            }},
            { "privacy", new List<string> {
                "Limit what you post on social media.",
                "Review app permissions often.",
                "Don't stay logged in on shared devices.",
                "Use browsers with privacy-focused extensions.",
                "Disable location tracking unless necessary.",
                "Be cautious when sharing your email address online."
            }},
            { "cybersecurity", new List<string> {
                "Use strong, unique passwords.",
                "Enable two-factor authentication.",
                "Keep your apps and OS updated.",
                "Lock your screen when away from your device.",
                "Use antivirus and anti-malware software.",
                "Be aware of social engineering tactics."
            }},
            { "antivirus", new List<string> {
                "Scan your system regularly.",
                "Don’t disable real-time protection.",
                "Update your antivirus definitions frequently.",
                "Schedule automatic scans weekly.",
                "Install antivirus from trusted sources only.",
                "Avoid using multiple antivirus tools at once."
            }},
            { "social engineering", new List<string> {
                "Don’t trust unexpected messages or calls.",
                "Verify who you're talking to before sharing info.",
                "Always be cautious with personal data requests.",
                "Don’t post personal details publicly online.",
                "Criminals may impersonate authority figures.",
                "Think before you act on emotionally urgent requests."
            }},
            { "scam", new List<string> {
                "Don’t send money to strangers.",
                "Scams often offer 'too good to be true' deals.",
                "Report scams and block the sender.",
                "Look out for spelling errors and urgency.",
                "Research any unknown caller or sender.",
                "Never give out OTPs or banking details via call."
            }},
            { "ransomware", new List<string> {
                "Never pay the ransom — it encourages further attacks.",
                "Back up your important data regularly.",
                "Be cautious of email attachments and links.",
                "Keep all your software up to date.",
                "Disable macros in Microsoft Office by default.",
                "Use endpoint protection tools to detect threats."
            }},
            { "2fa", new List<string> {
                "Enable 2FA on all critical accounts.",
                "Use an authenticator app rather than SMS if possible.",
                "2FA adds an extra layer of protection beyond your password.",
                "Backup your recovery codes safely.",
                "Avoid using your main email as a recovery method.",
                "Don’t ignore 2FA alerts — act if something looks suspicious."
            }},
            { "spyware", new List<string> {
                "Avoid installing unknown software from pop-ups.",
                "Use anti-spyware tools and scan your device often.",
                "Be cautious when granting app permissions.",
                "Spyware can silently monitor your activity.",
                "Avoid cracked or pirated software — they often include spyware.",
                "Use browser extensions that block trackers."
            }},
            { "browser safety", new List<string> {
                "Use privacy-focused browsers with ad blockers.",
                "Clear your cookies and cache regularly.",
                "Avoid logging into personal accounts on public machines.",
                "Enable 'Do Not Track' features.",
                "Don’t allow browser to save passwords.",
                "Update your browser frequently for security patches."
            }},
            { "public wifi", new List<string> {
                "Avoid online banking on public Wi-Fi.",
                "Always use a VPN when connected to public networks.",
                "Turn off automatic Wi-Fi connections on your devices.",
                "Avoid file sharing while on public networks.",
                "Disable Wi-Fi and Bluetooth when not in use.",
                "Treat all public Wi-Fi as untrusted."
            }}
        };


    }

}



