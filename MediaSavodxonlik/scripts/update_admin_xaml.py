with open('AdminPanel.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

target = '''      <Button x:Name="SaveGlossaryButton" Content="Glossariyni saqlash" Click="SaveGlossary" Background="#214744"
              Foreground="#99F6E4" HorizontalAlignment="Left" Margin="0,8,0,0" Padding="12,8"/>'''

replacement = '''      <Button x:Name="SaveGlossaryButton" Content="Glossariyni saqlash" Click="SaveGlossary" Background="#214744"
              Foreground="#99F6E4" HorizontalAlignment="Left" Margin="0,8,0,0" Padding="12,8"/>

      <!-- Medi AI Tizim Kaliti Bloki -->
      <Border Margin="0,24,0,0" Background="#15263A" BorderBrush="#2DD4BF" BorderThickness="1.5" CornerRadius="12" Padding="16">
       <StackPanel>
        <DockPanel Margin="0,0,0,6">
         <TextBlock Text="🤖  Medi AI Tizim Kaliti (Tarqatish uchun)" FontSize="17" FontWeight="Bold" Foreground="#5EEAD4" VerticalAlignment="Center"/>
         <TextBlock x:Name="SystemKeyStatusLabel" Text="Hozirda kalit yo‘q" Foreground="#94A3B8" FontSize="11" HorizontalAlignment="Right" VerticalAlignment="Center"/>
        </DockPanel>
        <TextBlock Text="Bu yerga OpenAI API kalitini kiritib saqlasangiz, kalit shifrlanadi va dastur tarqatilganda barcha foydalanuvchilarda Medi yordamchisi avtomatik ishlaydi. Oddiy foydalanuvchilar kalitni ko‘ra olmaydi."
                   Foreground="#BACBDD" FontSize="12" TextWrapping="Wrap" Margin="0,0,0,10"/>
        <TextBlock Text="Yangi tizim API kaliti:" FontSize="12" Foreground="#94A3B8" Margin="0,0,0,4"/>
        <PasswordBox x:Name="AdminApiKeyInput" Background="#0E1828" Foreground="#F1F5F9" Padding="9" Margin="0,0,0,10"
                     AutomationProperties.Name="Medi AI tizim kaliti"/>
        <WrapPanel>
         <Button x:Name="SaveSystemKeyButton" Content="💾  Tizim kalitini saqlash" Click="SaveSystemApiKey"
                 Background="#2DD4BF" Foreground="#082C30" FontWeight="SemiBold" Padding="14,8" Margin="0,0,10,0"/>
         <Button x:Name="ClearSystemKeyButton" Content="🗑  Kalitni o‘chirish" Click="ClearSystemApiKey"
                 Foreground="#FDA4AF" BorderBrush="#744355" BorderThickness="1" Padding="12,8"/>
        </WrapPanel>
       </StackPanel>
      </Border>'''

assert target in text, "target not found"
text = text.replace(target, replacement)

with open('AdminPanel.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("AdminPanel.xaml updated successfully!")
