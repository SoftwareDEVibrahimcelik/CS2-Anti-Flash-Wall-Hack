Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System
Imports System.Windows
Public Class devibrahimcelik

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("https://www.youtube.com/@devibrahimcelik")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        My.Computer.Clipboard.SetText("3H2iUqWmQ2RGTYDjJwceVeEFT8XMTafjrk")
        MsgBox("Copied DEV IBRAHIM CELIK is Bitcoin Address")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      

        On Error Resume Next


        Dim a As Process = Process.GetProcessesByName("cs2")(0)
        For Each amodule As System.Diagnostics.ProcessModule In a.Modules
            If amodule.FileName.IndexOf("matchmaking.dll") <> -1 Then
                ibrahimcelik.Text = amodule.BaseAddress.ToString
            End If
        Next

        Dim a2 As Process = Process.GetProcessesByName("cs2")(0)
        For Each amodule2 As System.Diagnostics.ProcessModule In a2.Modules
            If amodule2.FileName.IndexOf("client.dll") <> -1 Then
                Label1.Text = amodule2.BaseAddress.ToString
            End If
        Next




        'Github: https://github.com/SoftwareDEVibrahimcelik
        'YouTube: https://www.youtube.com/@devibrahimcelik

        ' 


    End Sub

    Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click

        If CheckBox2.Checked = True Then
            WriteLong("cs2", ibrahimcelik.Text + &H823A82, 2336788624)
        End If

        If CheckBox2.Checked = False Then
            WriteLong("cs2", ibrahimcelik.Text + &H823A82, 2336800818)
        End If



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        On Error Resume Next
        WriteDMAInteger("cs2.exe", ibrahimcelik.Text + &H1D2398, {&H28, &H160, &H260, &H10, &H8, &H6F8}, 0, 6)

        'devibrahimcelik
        'Interval default = 100 'you can decrease the value but im not recommend it. Because cpu&ram usage gonna be heigher. So its doesnt need it.

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        If CheckBox1.Checked = True Then
            Timer1.Start()
        End If

        If CheckBox1.Checked = False Then
            Timer1.Stop()
        End If
    End Sub
End Class
