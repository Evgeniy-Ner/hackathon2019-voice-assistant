﻿using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace Lib.Models.Speech.MicrosoftApi
{
    public class MicrosoftSynthesizer : ISynthesizer
    {
        private readonly string voiceName;

        public MicrosoftSynthesizer(string voiceName)
        {
            this.voiceName = voiceName;
        }

        public byte[] Synthesize(string text)
        {
            var task = Task.Run(() =>
            {
                using (var synth = new SpeechSynthesizer())
                using (var stream = new MemoryStream())
                {
                    synth.Rate = 1;
                    synth.Volume = 100;

                    synth.SelectVoice(voiceName);

                    synth.SetOutputToWaveStream(stream);
                    synth.Speak(text);
                    
                    return stream.GetBuffer();
                }
            });

            return task.Result;
        }
    }
}