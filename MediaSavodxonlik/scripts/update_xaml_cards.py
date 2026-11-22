import os

with open('MainWindow.xaml', 'r', encoding='utf-8') as f:
    content = f.read()

# Let's find TopicsList and replace its DataTemplate
start_tag = '<ItemsControl x:Name="TopicsList">'
end_tag = '</ItemsControl>'

start_idx = content.find(start_tag)
end_idx = content.find(end_tag, start_idx) + len(end_tag)

if start_idx != -1 and end_idx != -1:
    new_topics_list = '''<ItemsControl x:Name="TopicsList">
        <ItemsControl.ItemContainerStyle><Style TargetType="ContentPresenter"><EventSetter Event="Loaded" Handler="TopicCardLoaded"/></Style></ItemsControl.ItemContainerStyle>
        <ItemsControl.ItemsPanel><ItemsPanelTemplate><UniformGrid Columns="3"/></ItemsPanelTemplate></ItemsControl.ItemsPanel>
        <ItemsControl.ItemTemplate><DataTemplate>
         <Button Margin="0,0,14,14" Tag="{Binding Number}" Click="OpenTopic" HorizontalContentAlignment="Stretch" AutomationProperties.Name="{Binding Title}" Padding="0" Background="Transparent" BorderThickness="0" MouseEnter="TopicHover" MouseLeave="TopicLeave" GotKeyboardFocus="TopicHover" LostKeyboardFocus="TopicLeave">
          <Button.RenderTransform><TranslateTransform/></Button.RenderTransform>
          <Border CornerRadius="18" Padding="20" Background="#DD111D2E" BorderBrush="#354966" BorderThickness="1.5">
           <Grid>
            <!-- Watermark Big Number -->
            <TextBlock Text="{Binding NumberDisplay}" HorizontalAlignment="Right" VerticalAlignment="Top"
                       FontSize="84" FontWeight="Black" Margin="0,-24,-6,0" Opacity="0.07"
                       Foreground="{Binding Accent}" IsHitTestVisible="False"/>

            <StackPanel>
             <!-- Card Header: Pill Badge & Topic Icon & Track -->
             <DockPanel LastChildFill="True">
              <!-- Big Number Badge Pill -->
              <Border DockPanel.Dock="Right" CornerRadius="12" Padding="12,5" VerticalAlignment="Top" BorderThickness="1.5">
               <Border.Background>
                <SolidColorBrush Color="#18253A"/>
               </Border.Background>
               <Border.BorderBrush>
                <SolidColorBrush Color="{Binding Accent}"/>
               </Border.BorderBrush>
               <StackPanel Orientation="Horizontal">
                <TextBlock Text="MAVZU " FontSize="10" FontWeight="Bold" Foreground="#94A3B8" VerticalAlignment="Center"/>
                <TextBlock Text="{Binding NumberDisplay}" FontSize="18" FontWeight="Black" Foreground="{Binding Accent}" VerticalAlignment="Center"/>
               </StackPanel>
              </Border>

              <!-- Track Icon & Name -->
              <StackPanel Orientation="Horizontal" VerticalAlignment="Center">
               <Border Width="42" Height="42" CornerRadius="12" Background="#1B2D45" BorderBrush="#3B5274" BorderThickness="1" Margin="0,0,12,0">
                <TextBlock Text="{Binding Icon}" FontFamily="Segoe MDL2 Assets" FontSize="19" Foreground="{Binding Accent}" HorizontalAlignment="Center" VerticalAlignment="Center"/>
               </Border>
               <StackPanel VerticalAlignment="Center">
                <TextBlock Text="{Binding Track}" Foreground="#CBD5E1" FontSize="13" FontWeight="SemiBold"/>
                <TextBlock Text="{Binding Schedule}" Foreground="#94A3B8" FontSize="11" Margin="0,2,0,0"/>
               </StackPanel>
              </StackPanel>
             </DockPanel>

             <!-- Title -->
             <TextBlock Text="{Binding Title}" TextWrapping="Wrap" FontSize="18" FontWeight="Bold" Foreground="#F8FAFC" Margin="0,18,0,16" LineHeight="24" MinHeight="48"/>

             <!-- Divider -->
             <Border Height="1" Background="#263852" Margin="0,0,0,14"/>

             <!-- Footer: Status Pill & Action Link -->
             <DockPanel VerticalAlignment="Center">
              <Border DockPanel.Dock="Left" CornerRadius="8" Padding="8,4" Background="#16253B" BorderBrush="#2C405E" BorderThickness="1">
               <StackPanel Orientation="Horizontal">
                <Ellipse Width="6" Height="6" Fill="{Binding Accent}" Margin="0,0,6,0" VerticalAlignment="Center"/>
                <TextBlock Text="MODUL DARSI" FontSize="10" FontWeight="Bold" Foreground="#94A3B8" VerticalAlignment="Center"/>
               </StackPanel>
              </Border>
              <StackPanel Orientation="Horizontal" HorizontalAlignment="Right" VerticalAlignment="Center">
               <TextBlock Text="Darsni boshlash" Foreground="{Binding Accent}" FontSize="13" FontWeight="Bold" VerticalAlignment="Center" Margin="0,0,6,0"/>
               <TextBlock Text="→" Foreground="{Binding Accent}" FontSize="15" FontWeight="Bold" VerticalAlignment="Center"/>
              </StackPanel>
             </DockPanel>
            </StackPanel>
           </Grid>
          </Border>
         </Button>
        </DataTemplate></ItemsControl.ItemTemplate>
       </ItemsControl>'''
    content = content[:start_idx] + new_topics_list + content[end_idx:]
    with open('MainWindow.xaml', 'w', encoding='utf-8') as f:
        f.write(content)
    print("MainWindow.xaml updated successfully!")
else:
    print("Could not find TopicsList tags.")
