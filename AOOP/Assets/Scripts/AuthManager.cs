using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro; // Assuming you are using TextMeshPro for modern Unity UI

public class AuthManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText;

    [Header("Server Configuration")]
    public string backendUrl = "http://localhost:8080/api/auth";

    // Call this from your "Register" button's OnClick event
    public void OnRegisterButtonClicked()
    {
        StartCoroutine(SendAuthRequest("/register", usernameInput.text, passwordInput.text));
    }

    // Call this from your "Login" button's OnClick event
    public void OnLoginButtonClicked()
    {
        StartCoroutine(SendAuthRequest("/login", usernameInput.text, passwordInput.text));
    }

    private IEnumerator SendAuthRequest(string endpoint, string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Username and password cannot be empty!";
            feedbackText.color = Color.red;
            yield break;
        }

        feedbackText.text = "Connecting to server...";
        feedbackText.color = Color.yellow;

        // Create the form data for POST request
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        // Send the HTTP POST request to the backend
        string fullUrl = backendUrl + endpoint;
        using (UnityWebRequest www = UnityWebRequest.Post(fullUrl, form))
        {
            // For Basic Auth (if your login requires sending credentials in header)
            if (endpoint == "/login")
            {
                // Spring Security Basic Auth usually expects a Base64 Authorization header
                string auth = username + ":" + password;
                string encodedAuth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
                www.SetRequestHeader("Authorization", "Basic " + encodedAuth);
            }

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // Request failed (e.g., wrong password, server offline)
                feedbackText.text = "Error: " + www.downloadHandler.text;
                if (string.IsNullOrEmpty(feedbackText.text)) 
                {
                    feedbackText.text = "Connection failed: " + www.error;
                }
                feedbackText.color = Color.red;
                Debug.LogError("Auth Error: " + www.error + "\n" + www.downloadHandler.text);
            }
            else
            {
                // Request succeeded!
                feedbackText.text = "Success: " + www.downloadHandler.text;
                feedbackText.color = Color.green;
                Debug.Log("Auth Success: " + www.downloadHandler.text);

                if (endpoint == "/login")
                {
                    // TODO: Save credentials or move to the Main Menu scene!
                    // UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                }
            }
        }
    }
}
