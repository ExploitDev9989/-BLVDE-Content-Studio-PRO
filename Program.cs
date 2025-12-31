// ============================================================================
// BLVDE Content Studio - Program Entry Point
// ============================================================================
// This is where the program starts!
// When you run the .exe, this code executes first
// ============================================================================

using System;
using System.Windows.Forms;

namespace BLVDEContentStudio
{
    // ========================================================================
    // PROGRAM CLASS - The starting point of the application
    // ========================================================================
    internal static class Program
    {
        // ====================================================================
        // MAIN METHOD - This is the FIRST thing that runs
        // ====================================================================
        // [STAThread] means "Single Threaded Apartment" - required for Windows Forms
        // It tells Windows how to handle UI threading
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern Windows look
            // This makes buttons, text boxes, etc. look nice and modern
            Application.EnableVisualStyles();
            
            // Use default text rendering (makes text look crisp)
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Create and run the main form (window)
            // This line creates your BLVDE Content Studio window and shows it
            // The program will keep running until the window is closed
            Application.Run(new LoginForm());
            
            // When the window closes, the program ends here
        }
    }
}