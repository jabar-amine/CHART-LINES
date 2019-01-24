Public Function CENTER_IMAGE_H(ByVal IMG As Image, ByVal HAUT As Integer) As Image
        Dim bmp As New Bitmap(IMG.Width, HAUT)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(IMG, 0, CInt((HAUT - IMG.Height) / 2), IMG.Width, IMG.Height)
        Return bmp
End Function

Public Function CENTER_IMAGE(ByVal IMG As Image, ByVal LARGE As Integer) As Image
        Dim bmp As New Bitmap(LARGE, IMG.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(IMG, CInt((LARGE - IMG.Width) / 2), 0, IMG.Width, IMG.Height)
        Return bmp
End Function

 Public Function DRAW_CONCAT_IMAGES(ByVal IMG_I As Image, ByVal IMG_II As Image, ByVal ESPACE As Integer, ByVal HAUT As Integer) As Image
        Dim bmp As New Bitmap((IMG_I.Width + IMG_II.Width + ESPACE), HAUT)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(IMG_I, 0, 0, IMG_I.Width, IMG_I.Height)
        g.DrawImage(IMG_II, IMG_I.Width + ESPACE, 0, IMG_II.Width, IMG_II.Height)
        Return bmp
End Function

 Function GET_IMAGE_EFFICTIVES() As Image
        Dim IMG As Image
        IMG = DRAW_TEXT_LARGE(GET_EFFECTIVE("M"), Color.FromArgb(176, 183, 249), Form1.FONT_CHIFFRE.Font, FontStyle.Bold, 35, HAUTEUR, DRAW_FORMAT_RIGHT)
        IMG = DRAW_CONCAT_IMAGES(IMG, CENTER_IMAGE(CENTER_IMAGE_H(Image.FromStream(New MemoryStream(GLOBALE_SEXE_M)), HAUTEUR), 30), 0, HAUTEUR)
        IMG = DRAW_CONCAT_IMAGES(IMG, DRAW_TEXT_LARGE(GET_EFFECTIVE("F"), Color.FromArgb(253, 152, 205), Form1.FONT_CHIFFRE.Font, FontStyle.Bold, 35, HAUTEUR, DRAW_FORMAT_RIGHT), 0, HAUTEUR)
        IMG = DRAW_CONCAT_IMAGES(IMG, CENTER_IMAGE(CENTER_IMAGE_H(Image.FromStream(New MemoryStream(GLOBALE_SEXE_F)), HAUTEUR), 30), 0, HAUTEUR)
        IMG = DRAW_CONCAT_IMAGES(IMG, DRAW_TEXT_LARGE(GET_EFFECTIVE("ALL"), Color.DarkGray, Form1.FONT_CHIFFRE.Font, FontStyle.Bold, 35, HAUTEUR, DRAW_FORMAT_RIGHT), 0, HAUTEUR)
        IMG = DRAW_CONCAT_IMAGES(IMG, CENTER_IMAGE(CENTER_IMAGE_H(Image.FromStream(New MemoryStream(GLOBALE_IMG_CHART_SEXE)), HAUTEUR), 30), 0, HAUTEUR)
        Return IMG
End Function

Public Function DRAW_TEXT_LARGE(ByVal TXT As String, ByVal CLR As Color, ByVal FNT As Font, ByVal FSTY As FontStyle, ByVal LARGE As Integer, ByVal HAUT As Integer, ByVal drawFormat As System.Drawing.StringFormat) As Image
        Dim bmp As New Bitmap(LARGE, HAUT)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        Dim drawBrush As New SolidBrush(CLR)
        Dim drawFont As New Font(FNT, FSTY)
        Dim drawRect As New RectangleF(0, CInt((HAUT - 16) / 2), LARGE, 16)
        g.DrawString(TXT, drawFont, drawBrush, drawRect, drawFormat)
        Return bmp
End Function

Function GET_EFFECTIVE(ByVal SEXE As String) As Integer
        Dim res As Integer = 0
        If SEXE = "M" Then
            For Each Ligne As DataRow In foundRows_statistiques
                res = res + Val(Ligne("maleCount"))
            Next
        Else
            If SEXE = "F" Then
                For Each Ligne As DataRow In foundRows_statistiques
                    res = res + Val(Ligne("femaleCount"))
                Next
            Else
                For Each Ligne As DataRow In foundRows_statistiques
                    res = res + Val(Ligne("femaleCount")) + Val(Ligne("maleCount"))
                Next
            End If
        End If
        Return res
End Function

Public Function DRAW_BACKGOUND(ByVal CLR_BACKGOUND As Color, ByVal HAUTEUR As Integer, ByVal LARGE As Integer) As Image
        Dim bmp As New Bitmap(LARGE, HAUTEUR)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.FillRectangle(New SolidBrush(CLR_BACKGOUND), New RectangleF(0, 0, LARGE, HAUTEUR))
        Return bmp
End Function

Public Function DRAW_OVER_IMAGE(ByVal IMG_I As Image, ByVal IMG_II As Image) As Image
        Dim bmp As New Bitmap(IMG_I.Width, IMG_I.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawImage(IMG_I, 0, 0, IMG_I.Width, IMG_I.Height)
        g.DrawImage(IMG_II, 0, 0, IMG_II.Width, IMG_II.Height)
        Return bmp
End Function

Function SHOW_LINES_EFFECTIVE(ByVal HAUT As Integer, ByVal LARGE As Integer) As Image
        Dim IMG As Image
        Dim IMG_AGE As Image
        Dim IMG_F As Image
        Dim IMG_M As Image
        Dim COL_DATA_F As New Collection
        Dim COL_DATA_M As New Collection
        Dim COL_DATA_AGE As New Collection
        Dim X_MARGE As Integer = 20
        Dim Y_MARGE As Integer = 20
        Dim CHART_LARGE As Integer = LARGE ' - (2 * X_MARGE))
        Dim CHART_HAUT As Integer = HAUT ' - (2 * Y_MARGE))
        Dim X_M As Integer = CHART_HAUT - 24
        Dim X_F As Integer = CHART_HAUT - 24
        Dim X_AGE As Integer = CHART_HAUT - 24
        Dim POINTS_F As Point() = {New Point(Y_MARGE, X_F)}
        Dim POINTS_M As Point() = {New Point(Y_MARGE, X_M)}
        Dim POINTS_AGE As Point() = {New Point(Y_MARGE, X_AGE)}
        COL_DATA_F.Add("img")
        COL_DATA_M.Add("img")
        COL_DATA_AGE.Add("img")
        Dim X_STEP As Integer = 0
        Dim X_SPACE As Integer = CInt((CHART_LARGE - (3 * X_MARGE)) / foundRows_statistiques.Length) 'distinct ages
        Dim Y_SPACE As Integer = CInt((X_AGE - 50) / (GET_Y_STEP())) 'CInt((X_AGE ) / (GET_Y_STEP() + 2)) 'distint femaleCount,maleCount
        For Each Ligne As DataRow In foundRows_statistiques
            X_STEP = X_STEP + X_SPACE
            If Ligne("femaleCount") <> 0 Then
                Array.Resize(POINTS_F, POINTS_F.Length + 1)
                POINTS_F.SetValue(New Point(X_STEP + X_MARGE, X_F - GET_Y_DATA(Ligne("femaleCount"), Y_SPACE, Y_MARGE)), POINTS_F.Length - 1)
                COL_DATA_F.Add(Ligne("femaleCount"))
            End If
            If Ligne("maleCount") <> 0 Then
                Array.Resize(POINTS_M, POINTS_M.Length + 1)
                POINTS_M.SetValue(New Point(X_STEP + X_MARGE, X_M - GET_Y_DATA(Ligne("maleCount"), Y_SPACE, Y_MARGE)), POINTS_M.Length - 1)
                COL_DATA_M.Add(Ligne("maleCount"))
            End If
            Array.Resize(POINTS_AGE, POINTS_AGE.Length + 1)
            POINTS_AGE.SetValue(New Point(X_STEP + X_MARGE, X_AGE), POINTS_AGE.Length - 1)
            COL_DATA_AGE.Add(Ligne("age"))
        Next
        IMG = DRAW_BACKGOUND(Color.White, CHART_HAUT, CHART_LARGE)
        If POINTS_F.Length > 1 Then
            IMG_F = DRAW_LINE_SMOOTH_POINT(POINTS_F, Image.FromStream(New MemoryStream(GLOBALE_SEXE_F)), COL_DATA_F, Color.FromArgb(253, 152, 205), Color.White, Color.FromArgb(253, 152, 205), Color.DarkGray, 2, 32, 4, "DOWN", CHART_LARGE, CHART_HAUT, 32, POINTS_M)
            IMG = DRAW_OVER_IMAGE(IMG, IMG_F)
        End If
        If POINTS_M.Length > 1 Then
            IMG_M = DRAW_LINE_SMOOTH_POINT(POINTS_M, Image.FromStream(New MemoryStream(GLOBALE_SEXE_M)), COL_DATA_M, Color.FromArgb(176, 183, 249), Color.White, Color.FromArgb(176, 183, 249), Color.DarkGray, 2, 32, 4, "TOP", CHART_LARGE, CHART_HAUT, 32, POINTS_F)
            IMG = DRAW_OVER_IMAGE(IMG, IMG_M)
        End If
        If POINTS_AGE.Length > 1 Then
            IMG_AGE = DRAW_LINE_SMOOTH_POINT(POINTS_AGE, Image.FromStream(New MemoryStream(GLOBALE_IMG_CHART_SEXE)), COL_DATA_AGE, Color.WhiteSmoke, Color.White, Color.WhiteSmoke, Color.DarkGray, 2, 32, 4, "DOWN", CHART_LARGE, CHART_HAUT, 32)
            IMG = DRAW_OVER_IMAGE(IMG, IMG_AGE)
        End If
        Return IMG
End Function

Public Function DRAW_LINE_SMOOTH_POINT(ByVal points As Point(), ByVal IMG As Image, ByVal COL_DATA As Collection, ByVal CLR_LINE As Color, ByVal CLR_CIRCLE_ICON As Color, ByVal CLR_CIRCLE As Color, ByVal CLR_DATA As Color, ByVal LINE As Integer, ByVal LARGE_CIRCLE As Integer, ByVal LARGE_POINT As Integer, ByVal POSITION_DATA As String, ByVal LARGE As Integer, ByVal HAUT As Integer, ByVal LARGE_DATA As Integer, ByVal points_autres As Point()) As Image
        Dim bmp As New Bitmap(LARGE, HAUT)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
        g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
        g.DrawCurve(New Pen(CLR_LINE, LINE), points)
        Dim i As Integer = 1
        Dim POSITION_X As Integer = 0
        Dim POSITION_Y As Integer = 0
        Dim POSITION_Y_AUTRE As Integer = 0
        Dim ITEM_DATA As String = ""
        For Each P As Point In points
            ITEM_DATA = COL_DATA.Item(i).ToString
            If ITEM_DATA = "img" Then
                g.FillEllipse(Brushes.White, New RectangleF(P.X - CInt(LARGE_CIRCLE / 2), P.Y - CInt(LARGE_CIRCLE / 2), LARGE_CIRCLE, LARGE_CIRCLE))
                g.DrawEllipse(New Pen(CLR_CIRCLE_ICON, LINE), New RectangleF(P.X - CInt(LARGE_CIRCLE / 2), P.Y - CInt(LARGE_CIRCLE / 2), LARGE_CIRCLE, LARGE_CIRCLE))
                g.DrawImage(IMG, P.X - CInt(IMG.Width / 2), P.Y - CInt(IMG.Height / 2), IMG.Width, IMG.Height)
            Else
                g.FillEllipse(Brushes.White, New RectangleF(P.X - CInt(LARGE_POINT / 2), P.Y - CInt(LARGE_POINT / 2), LARGE_POINT, LARGE_POINT))
                g.DrawEllipse(New Pen(CLR_CIRCLE, LINE), New RectangleF(P.X - CInt(LARGE_POINT / 2), P.Y - CInt(LARGE_POINT / 2), LARGE_POINT, LARGE_POINT))

                POSITION_Y_AUTRE = GET_POSITION_Y(points_autres, P.X)
                If POSITION_Y_AUTRE <> 0 Then
                    If POSITION_Y_AUTRE = P.Y Then
                        POSITION_DATA = "TOP"
                    Else
                        If POSITION_Y_AUTRE > P.Y Then
                            POSITION_DATA = "TOP"
                        End If
                        If POSITION_Y_AUTRE < P.Y Then
                            POSITION_DATA = "DOWN"
                        End If
                    End If
                Else 'encas de autre valeur egale 0 
                    POSITION_DATA = "TOP"
                End If

                If POSITION_DATA = "TOP" Then
                    POSITION_X = P.X - CInt(LARGE_CIRCLE / 2) + LINE + CInt(LINE / 2)
                    POSITION_Y = P.Y - (CInt(LARGE_POINT / 2) + 16 + 6)
                Else
                    If POSITION_DATA = "DOWN" Then
                        POSITION_X = P.X - CInt(LARGE_CIRCLE / 2) + LINE + CInt(LINE / 2)
                        POSITION_Y = P.Y + CInt(LARGE_POINT / 2) + 6
                    Else
                        If POSITION_DATA = "LEFT" Then
                            POSITION_X = P.X - (CInt(LARGE_POINT / 2) + LARGE_CIRCLE + 6)
                            POSITION_Y = P.Y - CInt(LARGE_CIRCLE / 2)
                        Else
                            If POSITION_DATA = "RIGHT" Then
                                POSITION_X = P.X + (CInt(LARGE_POINT / 2) + 6)
                                POSITION_Y = P.Y - CInt(LARGE_CIRCLE / 2)
                            Else
                                POSITION_X = P.X - CInt(LARGE_CIRCLE / 2) + LINE + CInt(LINE / 2)
                                POSITION_Y = P.Y - CInt(LARGE_CIRCLE / 2)
                            End If
                        End If
                    End If
                End If
                g.DrawString(ITEM_DATA, New Font(Form1.FONT_CHIFFRE.Font, FontStyle.Regular), New SolidBrush(CLR_DATA), New RectangleF(POSITION_X, POSITION_Y, LARGE_DATA - (2 * LINE), 16), DRAW_FORMAT_CENTER)
            End If
            i = i + 1
        Next
        Return bmp
End Function


 Function GET_Y_STEP() As Integer
        COL_COUNT_STEP.Clear()
        Dim S As Integer = 0
        Dim i, j As Integer
        Dim EXIST As Boolean = False
        For Each Ligne As DataRow In foundRows_statistiques
            EXIST = False
            If Ligne("femaleCount") <> 0 Then
                For i = 1 To COL_COUNT_STEP.Count
                    If COL_COUNT_STEP.Item(i) = Ligne("femaleCount") Then
                        EXIST = True
                        Exit For
                    End If
                Next
                If EXIST = False Then
                    COL_COUNT_STEP.Add(Ligne("femaleCount"))
                End If
            End If

            EXIST = False
            If Ligne("maleCount") <> 0 Then
                For i = 1 To COL_COUNT_STEP.Count
                    If COL_COUNT_STEP.Item(i) = Ligne("maleCount") Then
                        EXIST = True
                        Exit For
                    End If
                Next
                If EXIST = False Then
                    COL_COUNT_STEP.Add(Ligne("maleCount"))
                End If
            End If
        Next
        Dim T_STEP(COL_COUNT_STEP.Count) As Integer
        Dim N As Integer = COL_COUNT_STEP.Count
        For i = 1 To COL_COUNT_STEP.Count
            T_STEP(i - 1) = COL_COUNT_STEP.Item(i)
        Next
        Dim Temp As Integer
        For i = 0 To N - 1
            For j = 0 To N - 1
                If T_STEP(j) > T_STEP(j + 1) Then
                    Temp = T_STEP(j)
                    T_STEP(j) = T_STEP(j + 1)
                    T_STEP(j + 1) = Temp  'Inverser si pas dans le bon ordre
                End If
            Next j
        Next i
        COL_COUNT_STEP.Clear()
        For i = 0 To N
            COL_COUNT_STEP.Add(T_STEP(i))
        Next i
        Return COL_COUNT_STEP.Count
    End Function

    Function GET_Y_DATA(ByVal DATA As Integer, ByVal I_STEP As Integer, ByVal MARGE As Integer) As Integer
        Dim S As Integer = MARGE
        For i = 1 To COL_COUNT_STEP.Count
            If COL_COUNT_STEP.Item(i) = DATA Then
                S = (I_STEP * i) + MARGE
            End If
        Next
        Return S
    End Function

    Function GET_POSITION_Y(ByVal points As Point(), ByVal position_x As Integer) As Integer
        Dim res As Integer = 0
        For Each P As Point In points
            If P.X = position_x Then
                res = P.Y
                Exit For
            End If
        Next
        Return res
    End Function




