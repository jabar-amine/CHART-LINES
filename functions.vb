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


