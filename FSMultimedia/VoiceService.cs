#if NETCOREAPP
using System;
using System.Speech.Recognition;
using System.Threading.Tasks;

namespace FSMultimedia
{
    public class VoiceService
    {
        private SpeechRecognitionEngine _recognizer;
        private TaskCompletionSource<string> _tcs;

        public VoiceService()
        {
            // Configuramos el motor de reconocimiento en español
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-ES"));

            // Usar el micrófono predeterminado del sistema
            _recognizer.SetInputToDefaultAudioDevice();

            // Cargar una gramática de dictado libre (permite decir cualquier cosa)
            Grammar dictationGrammar = new DictationGrammar();
            _recognizer.LoadGrammar(dictationGrammar);

            // Evento que se dispara cuando termina de procesar la voz
            _recognizer.SpeechRecognized += (s, e) =>
            {
                _tcs?.TrySetResult(e.Result.Text);
            };

            // Evento para cuando no entiende nada o hay silencio
            _recognizer.RecognizeCompleted += (s, e) =>
            {
                if (!_tcs.Task.IsCompleted) _tcs?.TrySetResult(string.Empty);
            };
        }

        public async Task<string> ListenOnceAsync()
        {
            _tcs = new TaskCompletionSource<string>();

            // Inicia un reconocimiento único
            _recognizer.RecognizeAsync(RecognizeMode.Single);

            return await _tcs.Task;
        }

        public void PlayText(string text)
        {             
            using (var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer())
            {
                synthesizer.SetOutputToDefaultAudioDevice();
                synthesizer.Speak(text);
            }
        }
    }
}
#endif