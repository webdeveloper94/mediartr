with open('MediChat.xaml', 'r', encoding='utf-8') as f:
    text = f.read()

old_expander = '<Expander Header="API kalitini sozlash" Foreground="#BAC8D9" FontSize="11" Margin="0,12,0,8"><StackPanel Margin="0,8"><TextBlock Text="OpenAI API kaliti (faqat joriy ishga tushirish uchun)" TextWrapping="Wrap"/><PasswordBox x:Name="ApiKey" Background="#0B1828" Foreground="#F1F5F9" Padding="9" Margin="0,8" AutomationProperties.Name="OpenAI API kaliti"/><TextBlock Text="Kalit faylga yozilmaydi. OPENAI_API_KEY muhit sozlamasidan ham foydalanish mumkin." Foreground="#93A4BA" TextWrapping="Wrap"/></StackPanel></Expander>'

new_expander = '''<Expander Header="API kalitini sozlash" Foreground="#BAC8D9" FontSize="11" Margin="0,12,0,8">
    <StackPanel Margin="0,8">
     <TextBlock x:Name="SystemKeyStatus" Text="✓ Tizim API kaliti faol ulangan" Foreground="#5EEAD4" FontWeight="SemiBold" Margin="0,0,0,6"/>
     <TextBlock Text="Shaxsiy OpenAI kaliti (ixtiyoriy, kiritilmasa tizim kaliti ishlaydi):" TextWrapping="Wrap" Foreground="#94A3B8"/>
     <PasswordBox x:Name="ApiKey" Background="#0B1828" Foreground="#F1F5F9" Padding="9" Margin="0,6" AutomationProperties.Name="OpenAI API kaliti"/>
     <TextBlock Text="Kiritilgan shaxsiy kalit faqat joriy seansda foydalaniladi." Foreground="#64748B" FontSize="10" TextWrapping="Wrap"/>
    </StackPanel>
   </Expander>'''

assert old_expander in text, "old_expander not found"
text = text.replace(old_expander, new_expander)

with open('MediChat.xaml', 'w', encoding='utf-8') as f:
    f.write(text)

print("MediChat.xaml updated successfully!")
