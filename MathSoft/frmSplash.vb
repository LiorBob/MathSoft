Public NotInheritable Class frmSplash

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        flashPlayer.Movie = My.Computer.FileSystem.CurrentDirectory + "\splash.swf"

    End Sub

    Private Sub tmrSplash_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrSplash.Tick

        frmMath.Show()

    End Sub

End Class
