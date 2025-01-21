
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ColorGenerator
{
    public partial class MainPage : ContentPage
    {
        // Color Instantiation
        Color color;
        public MainPage()
        {
            InitializeComponent();

            // Apply shades already upon running
            ApplyShadesToFrames(color = Color.FromRgb(0,0,0)); // the shades that this will be based on is the color BLACK
        }

        private void ApplyShadesToFrames(Color color)
        {
            // Create different shades of the base color
            Color shade1 = Color.FromHsla(color.GetHue(), color.GetSaturation(), 0.2, 1); 
            Color shade2 = Color.FromHsla(color.GetHue(), color.GetSaturation(), 0.4, 1);
            Color shade3 = Color.FromHsla(color.GetHue(), color.GetSaturation(), 0.6, 1);
            Color shade4 = Color.FromHsla(color.GetHue(), color.GetSaturation(), 0.8, 1);

            // Assign the base color to the main Container
            Container.BackgroundColor = color;

            // Assign the generated shades to the FrameDisplays
            FrameDisplay1.BackgroundColor = shade1;
            FrameDisplay2.BackgroundColor = shade2;
            FrameDisplay3.BackgroundColor = shade3;
            FrameDisplay4.BackgroundColor = shade4;

            // Update hex code display with the base color
            lblHex1.Text = shade1.ToHex();
            lblHex2.Text = shade2.ToHex();
            lblHex3.Text = shade3.ToHex();
            lblHex4.Text = shade4.ToHex();
        }

        private void SetColorAndColorValue(Color color)
        {
            // Variable Declaration
            var redVal = Math.Round((color.Red * 255), 0);
            var greenVal = Math.Round((color.Green * 255), 0);
            var blueVal = Math.Round((color.Blue * 255), 0);

            // Displays the rgb values to the labels
            lblRed.Text = "Red Value: " + redVal;
            lblGreen.Text = "Green Value: " + greenVal;
            lblBlue.Text = "Blue Value: " + blueVal;

            // Sets the color
            btnGenerate.BorderColor = Container.BackgroundColor = color;

            // Change the value of the slider according to the hexValue
            sldRed.Value = color.Red;
            sldGreen.Value = color.Green;
            sldBlue.Value = color.Blue;

            // Apply Shades
            ApplyShadesToFrames(color);
        }

        // Creating an EventHandler that checks if the userInput is a valid value
        private bool IsHexColor(string hexValue)
        {
            // A condition wherein it checks for the userInput if it is valid or not
            if(string.IsNullOrEmpty(hexValue) || hexValue.Length != 7 || !hexValue.StartsWith("#")) 
            {
                return false; // if it is invalid it'll return false
            }
                
            return true; // otherwise, true
        }


        private void btnGenerate_Clicked(object sender, EventArgs e)
        {
            // Variable Declaration
            string hexValue = txtHex.Text;

            // Validate hex input
            if (IsHexColor(hexValue))
            {
                // Color instantiation
                color = Color.FromArgb(hexValue);

                // Sets the color
                SetColorAndColorValue(color);
            }
            else
            {
                // Handle invalid hex code
                DisplayAlert("Invalid Hex Code", "Please enter a valid hex color code (e.g. #FF5733).", "OK");
            }
        }

        private void Color_Button_Clicked(object sender, EventArgs e)
        {
            // Ensure sender is a Button
            if (sender is Button clickedButton)
            {
                // Declare variables
                int red = 0, green = 0, blue = 0;

                // Determine which button was clicked based on its ID or text
                switch (clickedButton.Text)
                {
                    // For the Red_Button
                    case "Red": 
                        red = 255;
                        break;

                    // For the Green_Button
                    case "Green":
                        green = 255;
                        break;

                    // For the Blue_Button
                    case "Blue":
                        blue = 255;
                        break;
                        
                    // For the White_Button
                    case "White":
                        red = 255; green = 255; blue = 255;
                        break;

                    // For the Black_Button
                    case "Black":
                        red = green = blue = 0;
                        break;
                }

                // Set slider values and the color
                sldRed.Value = red;
                sldGreen.Value = green;
                sldBlue.Value = blue;

                // Create the color from RGB values
                color = Color.FromRgb(red, green, blue);

                // Set the color and update hex value
                SetColorAndColorValue(color);
                txtHex.Placeholder = color.ToHex();
            }
        }
    }
}