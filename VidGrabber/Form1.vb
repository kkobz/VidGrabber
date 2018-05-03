Imports System.Net
Imports System.Text.RegularExpressions

Public Class Form1
    Dim webclient1 As New WebClient
    Dim urltemp As String
    Dim htmltemp As String
    Dim vidtitleval As String

    Public Property Vidtitleval1 As String
        Get
            Return vidtitleval
        End Get
        Set(value As String)
            vidtitleval = value
        End Set
    End Property

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        downloadbutt.Enabled = False
        vidname.Text = ""
        vidurl.Text = ""
    End Sub

    Private Sub gobutt_Click(sender As Object, e As EventArgs) Handles gobutt.Click
        urltemp = urlbox.Text
        Try
            htmltemp = webclient1.DownloadString(urltemp)
        Catch ex As Exception
            MessageBox.Show("No connection to the video provider!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            vidname.Text = ""
            vidurl.Text = ""
            htmltemp = ""
            vidtitleval = ""
            urltemp = ""
            urlbox.Text = ""
            gobutt.Enabled = True
            downloadbutt.Enabled = True
            Exit Sub
        End Try
        vidtitleval = Regex.Match(htmltemp, "\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups("Title").Value
        vidname.Text = vidtitleval
        vidurl.Text = urltemp
        gobutt.Enabled = False
        downloadbutt.Enabled = True
    End Sub

    Private Sub downloadbutt_Click(sender As Object, e As EventArgs) Handles downloadbutt.Click
        downloadbutt.Enabled = False
        Dim processstring As String
        processstring = "-f mp4 -o ""%USERPROFILE%\Videos\%(title)s.%(ext)s"" " + urltemp
        Try
            Process.Start("youtube-dl.exe", processstring)
        Catch ex As Exception
            MessageBox.Show("The file youtube-dl.exe could not be found or cannot be run!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            vidname.Text = ""
            vidurl.Text = ""
            htmltemp = ""
            vidtitleval = ""
            urltemp = ""
            urlbox.Text = ""
            gobutt.Enabled = True
            downloadbutt.Enabled = True
            Exit Sub
        End Try
        MessageBox.Show("Video was saved to Videos folder.", "Video Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
