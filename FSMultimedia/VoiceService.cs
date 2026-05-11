#if NETCOREAPP
using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace FSMultimedia
{
    public class VoiceService
    {
        private SpeechSynthesizer _synthesizer;
        private SpeechRecognitionEngine _recognizer;
        private TaskCompletionSource<string> _tcs;

        public VoiceService()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SetOutputToDefaultAudioDevice();

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

        /// <summary>
        /// Detiene inmediatamente cualquier reproducción de audio en curso.
        /// </summary>
        public void Stop()
        {
            try
            {
                // Aborta todas las locuciones que estén en la cola o reproduciéndose
                _synthesizer.SpeakAsyncCancelAll();
            }
            catch (Exception ex)
            {
                // Loguear el error si es necesario
                Console.WriteLine($"Error al detener el audio: {ex.Message}");
            }
        }

        public void PlayText(string text)
        {
            _synthesizer.SpeakAsyncCancelAll();
            _synthesizer.Speak(text);
        }

        public async Task PlayTextAsync(string text)
        {
            await Task.Run(() =>
            {
                _synthesizer.SpeakAsyncCancelAll();
                _synthesizer.SpeakAsync(text);
            });
        }
    }
}
#endif