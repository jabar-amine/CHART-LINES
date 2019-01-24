//******//

BGW_INFOS : is a Backgroundworker
PIC_INFOS_DATA : is a Picturebox
TIMER_PROGRES : is a Timer

//******//

Some other functions in (functions.vb)

////////////////////////////////////////////////////

Dim MyThread_data As System.ComponentModel.BackgroundWorker
Dim GLOBALE_IMG_CHART_SEXE() As Byte
GLOBALE_IMG_CHART_SEXE = File.ReadAllBytes(Application.StartupPath.Trim & "\images\CHART_SEXE.png")
Dim ID_LOADING_DATA As Integer = 0
Dim IMG_INFOS_DATA As Image
dim DRAW_FORMAT_LEFT As New StringFormat()
DRAW_FORMAT_LEFT.Alignment = StringAlignment.Near
Dim foundRows_statistiques() As Data.DataRow
Dim COL_COUNT_STEP As New Collection

////////////////////////////////////////////////////

Private Sub BGW_INFOS_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW_INFOS.DoWork
        MyThread_data = CType(sender, System.ComponentModel.BackgroundWorker)
        If MyThread_data.CancellationPending Then
            e.Cancel = True
        Else
            CHARGER_INFOS_DATA(MyThread_data, e)
        End If
End Sub

Private Sub BGW_INFOS_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW_INFOS.RunWorkerCompleted
        TIMER_PROGRES.Stop()
        SHOW_IMAGE_INFOS_DATA()
End Sub

Sub CHARGER_INFOS_DATA(ByVal MyThread As System.ComponentModel.BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs)
        ////////
        LOAD DATA FROM YOUR DATABASE
        ************
        Example : Json [
        {"age":"0","maleCount":"1","femaleCount":"0"},
        {"age":"2","maleCount":"5","femaleCount":"5"},
        {"age":"3","maleCount":"17","femaleCount":"17"},
        {"age":"4","maleCount":"26","femaleCount":"21"},
        {"age":"5","maleCount":"41","femaleCount":"31"},
        {"age":"6","maleCount":"36","femaleCount":"27"},
        {"age":"7","maleCount":"40","femaleCount":"28"},
        {"age":"8","maleCount":"28","femaleCount":"18"},
        {"age":"9","maleCount":"13","femaleCount":"9"},
        {"age":"10","maleCount":"10","femaleCount":"8"},
        {"age":"11","maleCount":"0","femaleCount":"7"}
        ]
        
        foundRows_statistiques = (Convert Json to DataRow)
      
        ////////
End Sub

Private Sub TIMER_PROGRES_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TIMER_PROGRES.Tick
        If ID_LOADING_DATA >= 100 Then
            ID_LOADING_DATA = 1
        Else
            ID_LOADING_DATA = ID_LOADING_DATA + 1
        End If
        SHOW_PROGRESSION()
End Sub

Sub SHOW_PROGRESSION()
        PIC_INFOS_DATA.Image = DRAW_PROGRESSION_LOADING(ID_LOADING_DATA, Color.FromArgb(126, 167, 236), Color.FromArgb(169, 219, 38), Color.FromArgb(254, 205, 10), 100)
End Sub

Sub SHOW_IMAGE_INFOS_DATA()
        PIC_INFOS_DATA.SizeMode = PictureBoxSizeMode.Normal
        Dim ROWS_DATA() As Data.DataRow
      
        DESC_PAGE.Text = "L'EFFECTIF DES ÉLÈVES"
        IMG_INFOS_DATA = GET_IMAGE_MENU_ITEM(Image.FromStream(New MemoryStream(GLOBALE_IMG_CHART_SEXE)), "L'EFFECTIF DES ÉLÈVES", PIC_INFOS_DATA.Width, GET_IMAGE_EFFICTIVES)
        ADD_IMAGE(IMG_INFOS_DATA, SHOW_LINES_EFFECTIVE(PIC_INFOS_DATA.Height - 40, PIC_INFOS_DATA.Width), False)
      
        PIC_INFOS_DATA.Image = IMG_INFOS_DATA
End Sub

Function GET_IMAGE_MENU_ITEM(ByRef ICON_IMG As Image, ByVal TITLE As String, ByVal FULL_LARGE As Integer, ByVal IMG_RIGHT As Image) As Image
        Dim IMG As Image
        IMG = CENTER_IMAGE(CENTER_IMAGE_H(ICON_IMG, HAUTEUR), 30)
        IMG = DRAW_CONCAT_IMAGES(IMG, DRAW_TEXT_LARGE(TITLE, Color.FromArgb(118, 148, 207), Form1.FONT_TEXT.Font, FontStyle.Regular, FULL_LARGE - (IMG_RIGHT.Width + 60), HAUTEUR, DRAW_FORMAT_LEFT), 10, HAUTEUR)
        IMG = DRAW_CONCAT_IMAGES(IMG, IMG_RIGHT, 10, HAUTEUR)
        Return IMG
End Function

